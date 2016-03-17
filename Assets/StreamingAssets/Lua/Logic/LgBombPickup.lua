--
-- Bomb pickup class.
--
-- @filename  LgBombPickup.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgBombPickup.
local strClassName = "LgBombPickup"
local LgBombPickup = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgBombPickup.this = false

-- The transform.
LgBombPickup.transform = false

-- The c# gameObject.
LgBombPickup.gameObject = false

-- Sound for when the bomb crate is picked up.
LgBombPickup.m_cPickupClip = false;

-- Reference to the animator component.
LgBombPickup.m_cAnim = false;

-- Whether or not the crate has landed yet.
LgBombPickup.bLanded = false;

-- Awake method.
function LgBombPickup:Awake()
    --print("LgBombPickup:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgBombPickup!")
        return
    end

    -- Setting up the reference.
    self.m_cAnim = self.transform.root:GetComponent(Animator)
end

-- OnTriggerEnter2D method.
function LgBombPickup:OnTriggerEnter2D(cOtherCollider2D)
    --print("LgBombPickup:OnTriggerEnter2D")

    -- If the player enters the trigger zone...
    if "Player" == cOtherCollider2D.tag then
        -- ... play the pickup sound effect.
        AudioSource.PlayClipAtPoint(self.m_cPickupClip, self.transform.position)

        -- Increase the number of bombs the player has.
        local cLayBombs = cOtherCollider2D:GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cLayBombs
        cLayBombs.m_nBombCount = cLayBombs.m_nBombCount + 1

        -- Destroy the crate.
        GameObject.Destroy(self.transform.root.gameObject)
    -- Otherwise if the crate lands on the ground...
    elseif ("ground" == cOtherCollider2D.tag) and (not self.m_bLanded) then
        -- ... set the animator trigger parameter Land.
        self.m_cAnim:SetTrigger("Land")
        self.transform.parent = nil
        self.gameObject:AddComponent(Rigidbody2D)
        self.m_bLanded = true
    end
end

-- Return this class.
return LgBombPickup
