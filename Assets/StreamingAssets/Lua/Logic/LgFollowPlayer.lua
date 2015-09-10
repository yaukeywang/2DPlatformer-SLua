--
-- Follow player class.
--
-- @filename  LgFollowPlayer.lua
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

-- Register new class LgFollowPlayer.
local strClassName = "LgFollowPlayer"
local LgFollowPlayer = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgFollowPlayer.this = false

-- The transform.
LgFollowPlayer.transform = false

-- The c# gameObject.
LgFollowPlayer.gameObject = false

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
end

-- Update method.
function LgFollowPlayer:Update()
    --print("LgFollowPlayer:Update")

    -- Set the position to the player's position with the offset.
    self.transform.position = self.m_cPlayer.position + self.m_vOffset
end

-- Return this class.
return LgFollowPlayer
