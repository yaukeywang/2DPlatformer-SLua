--
-- Follow player class.
--
-- @filename  LgFollowPlayer.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgFollowPlayer.
local strClassName = "LgFollowPlayer"
local LgFollowPlayer = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The offset at which the Health Bar follows the player.
LgFollowPlayer.m_vOffset = Vector3.zero

-- Reference to the player.
LgFollowPlayer.m_cPlayer = false

-- Awake method.
function LgFollowPlayer:Awake()
    --print("LgFollowPlayer:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgFollowPlayer!")
        return
    end

    -- Setting up the reference.
    self.m_cPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    local aFloatArray = cDataBridge.m_floats

    -- Get offset data.
    local vOffset = self.m_vOffset
    vOffset.x = aFloatArray[1]
    vOffset.y = aFloatArray[2]
    vOffset.z = aFloatArray[3]
end

-- Update method.
function LgFollowPlayer:Update()
    --print("LgFollowPlayer:Update")

    -- Set the position to the player's position with the offset.
    self.transform.position = self.m_cPlayer.position + self.m_vOffset
end

-- Return this class.
return LgFollowPlayer
