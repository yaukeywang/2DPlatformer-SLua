--
-- Health pickup class.
--
-- @filename  LgHealthPickup.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-07
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgHealthPickup.
local strClassName = "LgHealthPickup"
local LgHealthPickup = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgHealthPickup.this = false

-- The transform.
LgHealthPickup.transform = false

-- The c# gameObject.
LgHealthPickup.gameObject = false

-- Reference to the pickup spawner.
LgHealthPickup.m_cPickupSpawner = false

-- Reference to the animator component.
LgHealthPickup.m_cAnim = false

-- Whether or not the crate has landed.
LgHealthPickup.m_bLanded = false

-- Awake method.
function LgHealthPickup:Awake()
    --print("LgHealthPickup:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgHealthPickup!")
        return
    end

    -- Setting up the references.
    self.m_cPickupSpawner = GameObject.Find("pickupManager"):GetComponent(PickupSpawner)
    self.m_cAnim = self.transform.root:GetComponent(Animator)
end

-- OnTriggerEnter2D method.
function LgHealthPickup:OnTriggerEnter2D(cOtherCollider2D)
    --print("LgHealthPickup:OnTriggerEnter2D")

    local this = self.this

    -- If the player enters the trigger zone...
    if "Player" == cOtherCollider2D.tag then
        -- Get a reference to the player health script.
        local cPlayerHealth = cOtherCollider2D:GetComponent(PlayerHealth)

        -- Increasse the player's health by the health bonus but clamp it at 100.
        cPlayerHealth.m_health = cPlayerHealth.m_health + this.m_healthBonus
        --cPlayerHealth.health = Mathf.Clamp(cPlayerHealth.health, 0.0, 100.0) -- Match type error.
        if cPlayerHealth.m_health < 0.0 then
            cPlayerHealth.m_health = 0.0
        elseif cPlayerHealth.m_health > 100.0 then
            cPlayerHealth.m_health = 100.0
        end

        -- Update the health bar.
        cPlayerHealth:UpdateHealthBar()

        -- Trigger a new delivery.
        self.m_cPickupSpawner:DeliverPickup()

        -- Play the collection sound.
        AudioSource.PlayClipAtPoint(this.m_collect, self.transform.position);

        -- Destroy the crate.
        GameObject.Destroy(self.transform.root.gameObject)
    -- Otherwise if the crate hits the ground...
    elseif ("ground" == cOtherCollider2D.tag) and (not self.m_bLanded) then
        -- ... set the Land animator trigger parameter.
        self.m_cAnim:SetTrigger("Land")

        self.transform.parent = nil
        self.gameObject:AddComponent(Rigidbody2D)
        self.m_bLanded = true
    end
end

-- Return this class.
return LgHealthPickup
