--
-- Health pickup class.
--
-- @filename  LgHealthPickup.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-07
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgHealthPickup.
local strClassName = "LgHealthPickup"
local LgHealthPickup = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- How much health the crate gives the player.
LgHealthPickup.m_fHealthBonus = 0.0

-- The sound of the crate being collected.
LgHealthPickup.m_cCollectClip = nil

-- Reference to the pickup spawner.
LgHealthPickup.m_cPickupSpawner = nil

-- Reference to the animator component.
LgHealthPickup.m_cAnim = nil

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

    -- Get data bridge and set params.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    self.m_fHealthBonus = cDataBridge.m_floats[1]
    self.m_cCollectClip = cDataBridge.m_audioClips[1]
end

-- OnTriggerEnter2D method.
function LgHealthPickup:OnTriggerEnter2D(cOtherCollider2D)
    --print("LgHealthPickup:OnTriggerEnter2D")

    -- If the player enters the trigger zone...
    if "Player" == cOtherCollider2D.tag then
        -- Get a reference to the player health script.
        local cPlayerHealth = cOtherCollider2D:GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cPlayerHealth

        -- Increasse the player's health by the health bonus but clamp it at 100.
        cPlayerHealth.m_fHealth = cPlayerHealth.m_fHealth + self.m_fHealthBonus
        --cPlayerHealth.m_fHealth = Mathf.Clamp(cPlayerHealth.m_fHealth, 0.0, 100.0) -- Match type error.
        if cPlayerHealth.m_fHealth < 0.0 then
            cPlayerHealth.m_fHealth = 0.0
        elseif cPlayerHealth.m_fHealth > 100.0 then
            cPlayerHealth.m_fHealth = 100.0
        end

        -- Update the health bar.
        cPlayerHealth:UpdateHealthBar()

        -- Trigger a new delivery.
        self.m_cPickupSpawner:DeliverPickup()

        -- Play the collection sound.
        AudioSource.PlayClipAtPoint(self.m_cCollectClip, self.transform.position);

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
