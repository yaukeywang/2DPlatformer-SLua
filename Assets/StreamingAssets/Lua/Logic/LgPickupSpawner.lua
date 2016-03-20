--
-- Pickup spawner class.
--
-- @filename  LgPickupSpawner.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-07
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgPickupSpawner.
local strClassName = "LgPickupSpawner"
local LgPickupSpawner = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- Array of pickup prefabs with the bomb pickup first and health second.
LgPickupSpawner.m_aPickups = nil

-- Delay on delivery.
LgPickupSpawner.m_fPickupDeliveryTime = 5.0

-- Smallest value of x in world coordinates the delivery can happen at.
LgPickupSpawner.m_fDropRangeLeft = 0.0

-- Largest value of x in world coordinates the delivery can happen at.
LgPickupSpawner.m_fDropRangeRight = 0.0

-- The health of the player, above which only bomb crates will be delivered.
LgPickupSpawner.m_fHighHealthThreshold = 75.0

-- The health of the player, below which only health crates will be delivered.
LgPickupSpawner.m_fLowHealthThreshold = 25.0

-- Reference to the PlayerHealth script.
LgPickupSpawner.m_cPlayerHealth = nil

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
    self.m_cPlayerHealth = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cPlayerHealth

    -- Set pickup prefabs.
    self.m_aPickups = {}
    for i = 1, #self.m_aParameters do
        self.m_aPickups[i] = self.m_aParameters[i]
    end

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    local aFloatArray = cDataBridge.m_floats

    -- Set params.
    self.m_fPickupDeliveryTime = aFloatArray[1]
    self.m_fDropRangeLeft = aFloatArray[2]
    self.m_fDropRangeRight = aFloatArray[3]
    self.m_fHighHealthThreshold = aFloatArray[4]
    self.m_fLowHealthThreshold = aFloatArray[5]
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

    -- Create coroutine.
    local cCol = coroutine.create(function ()
        -- Wait for the delivery delay.
        Yield(WaitForSeconds(self.m_fPickupDeliveryTime))

        -- Get player health.
        local cPlayerHealth = self:GetPlayerHealth()

        -- Check if this object is destroyed.
        if self.m_bDestroy or (not cPlayerHealth) or Slua.IsNull(cPlayerHealth) then
            return
        end

        -- Create a random x coordinate for the delivery in the drop range.
        local fDropPosX = Random.Range(self.m_fDropRangeLeft, self.m_fDropRangeRight)

        -- Create a position with the random x coordinate.
        local vDropPos = Vector3(fDropPosX, 15.0, 1.0)

        -- Check player health.
        -- If the player's health is above the high threshold...
        if self.m_cPlayerHealth.m_fHealth >= self.m_fHighHealthThreshold then
            -- ... instantiate a bomb pickup at the drop position.
            GameObject.Instantiate(self.m_aPickups[1], vDropPos, Quaternion.identity)
        elseif self.m_cPlayerHealth.m_fHealth <= self.m_fLowHealthThreshold then
            -- ... instantiate a health pickup at the drop position.
            GameObject.Instantiate(self.m_aPickups[2], vDropPos, Quaternion.identity)
        else
            -- Otherwise...
            -- ... instantiate a random pickup at the drop position.
            local nPickupIndex = Random.Range(1, #self.m_aPickups + 1)
            GameObject.Instantiate(self.m_aPickups[nPickupIndex], vDropPos, Quaternion.identity)
        end
    end)

    -- Resume the coroutine.
    coroutine.resume(cCol)
end

-- Get player health.
function LgPickupSpawner:GetPlayerHealth()
    --print("LgPickupSpawner:GetPlayerHealth")
    if (not self.m_cPlayerHealth) and (not self.m_bDestroy) then
        self.m_cPlayerHealth = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cPlayerHealth
    end

    return self.m_cPlayerHealth
end

-- Return this class.
return LgPickupSpawner
