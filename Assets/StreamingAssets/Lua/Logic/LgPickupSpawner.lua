--
-- Pickup spawner class.
--
-- @filename  LgPickupSpawner.lua
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

-- Register new class LgPickupSpawner.
local strClassName = "LgPickupSpawner"
local LgPickupSpawner = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgPickupSpawner.this = false

-- The transform.
LgPickupSpawner.transform = false

-- The c# gameObject.
LgPickupSpawner.gameObject = false

-- Reference to the PlayerHealth script.
LgPickupSpawner.m_cPlayerHealth = false

-- The destroy flag.
LgPickupSpawner.m_bDestroy = false

-- Awake method.
function LgPickupSpawner:Awake()
    --print("LgPickupSpawner:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgPickupSpawner!")
        return
    end

    -- Init the destroy flag.
    self.m_bDestroy = false

    -- Setting up the reference.
    self.m_cPlayerHealth = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviourBase):GetLuaTable().m_cPlayerHealth
end

-- Start method.
function LgPickupSpawner:Start()
    --print("LgPickupSpawner:Start")

    -- Start the first delivery.
    self:DeliverPickup()
end

-- On destroy method.
function LgPickupSpawner:OnDestroy()
    --print("LgPickupSpawner:OnDestroy")
    self.m_bDestroy = true
end

-- The deliver pickup method.
function LgPickupSpawner:DeliverPickup()
    --print("LgPickupSpawner:DeliverPickup")

    -- Get player health.
    local cPlayerHealth = self:GetPlayerHealth()

    -- Check if this object is destroyed.
    if self.m_bDestroy or (not cPlayerHealth) or Slua.IsNull(cPlayerHealth) then
        return
    end

    -- Temp local
    local this = self.this

    -- Create coroutine.
    local cCol = coroutine.create(function ()
        -- Wait for the delivery delay.
        Yield(WaitForSeconds(this.m_pickupDeliveryTime))

        -- Get player health.
        local cPlayerHealth = self:GetPlayerHealth()

        -- Check if this object is destroyed.
        if self.m_bDestroy or (not cPlayerHealth) or Slua.IsNull(cPlayerHealth) then
            return
        end

        -- Create a random x coordinate for the delivery in the drop range.
        local fDropPosX = Random.Range(this.m_dropRangeLeft, this.m_dropRangeRight)

        -- Create a position with the random x coordinate.
        local vDropPos = Vector3(fDropPosX, 15.0, 1.0)

        -- Check player health.
        -- If the player's health is above the high threshold...
        if self.m_cPlayerHealth.m_fHealth >= this.m_highHealthThreshold then
            -- ... instantiate a bomb pickup at the drop position.
            GameObject.Instantiate(this.m_pickups[1], vDropPos, Quaternion.identity)
        elseif self.m_cPlayerHealth.m_fHealth <= this.m_lowHealthThreshold then
            -- ... instantiate a health pickup at the drop position.
            GameObject.Instantiate(this.m_pickups[2], vDropPos, Quaternion.identity)
        else
            -- Otherwise...
            -- ... instantiate a random pickup at the drop position.
            local nPickupIndex = Random.Range(1, #this.m_pickups)
            GameObject.Instantiate(this.m_pickups[nPickupIndex], vDropPos, Quaternion.identity)
        end
    end)

    -- Resume the coroutine.
    coroutine.resume(cCol)
end

-- Get player health.
function LgPickupSpawner:GetPlayerHealth()
    --print("LgPickupSpawner:GetPlayerHealth")
    if (not self.m_cPlayerHealth) and (not self.m_bDestroy) then
        self.m_cPlayerHealth = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviourBase):GetLuaTable().m_cPlayerHealth
    end

    return self.m_cPlayerHealth
end

-- Return this class.
return LgPickupSpawner
