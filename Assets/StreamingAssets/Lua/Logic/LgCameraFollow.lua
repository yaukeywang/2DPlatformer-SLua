--
-- The camera follow class.
--
-- @filename  LgCameraFollow.lua
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

-- Register new class LgCameraFollow.
local strClassName = "LgCameraFollow"
local LgCameraFollow = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgCameraFollow.this = false

-- The transform.
LgCameraFollow.transform = false

-- The c# gameObject.
LgCameraFollow.gameObject = false

-- Reference to the player's transform.
LgCameraFollow.m_cTrsPlayer = false

-- Awake method.
function LgCameraFollow:Awake()
    --print("LgCameraFollow:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgCameraFollow!")
        return
    end

    -- Setting up the reference.
    self.m_cTrsPlayer = GameObject.FindGameObjectWithTag("Player").transform
end

-- Fixed update method.
function LgCameraFollow:FixedUpdate()
    --print("LgCameraFollow:FixedUpdate")

    -- Check player first. (Maybe dead!)
    if Slua.IsNull(self.m_cTrsPlayer) then
        return
    end

    -- Track player.
    self:TrackPlayer()
end

-- Check x margin.
function LgCameraFollow:CheckXMargin()
    --print("LgCameraFollow:CheckXMargin")

    -- Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
    return math.abs(self.transform.position.x - self.m_cTrsPlayer.position.x) > self.this.m_xMargin
end

-- Check y margin.
function LgCameraFollow:CheckYMargin()
    --print("LgCameraFollow:CheckYMargin")
    
    -- Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
    return math.abs(self.transform.position.y - self.m_cTrsPlayer.position.y) > self.this.m_yMargin
end

-- Track player.
function LgCameraFollow:TrackPlayer()
    --print("LgCameraFollow:TrackPlayer")

    local this = self.this
    
    -- By default the target x and y coordinates of the camera are it's current x and y coordinates.
    local fTargetX = self.transform.position.x
    local fTargetY = self.transform.position.y

    -- If the player has moved beyond the x margin...
    if self:CheckXMargin() then
        -- ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
        fTargetX = Mathf.Lerp(self.transform.position.x, self.m_cTrsPlayer.position.x, self.this.m_xSmooth * Time.deltaTime)
    end

    -- If the player has moved beyond the y margin...
    if self:CheckYMargin() then
        -- ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
        fTargetY = Mathf.Lerp(self.transform.position.y, self.m_cTrsPlayer.position.y, self.this.m_ySmooth * Time.deltaTime)
    end

    -- The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
    -- Parameter match type error.
    --fTargetX = Mathf.Clamp(fTargetX, this.m_minXAndY.x, this.m_maxXAndY.x)
    --fTargetY = Mathf.Clamp(fTargetY, this.m_minXAndY.y, this.m_maxXAndY.y)

    if fTargetX < this.m_minXAndY.x then
        fTargetX = this.m_minXAndY.x
    elseif fTargetX > this.m_maxXAndY.x then
        fTargetX = this.m_maxXAndY.x
    end

    if fTargetY < this.m_minXAndY.y then
        fTargetY = this.m_minXAndY.y
    elseif fTargetY > this.m_maxXAndY.y then
        fTargetY = this.m_maxXAndY.y
    end

    -- Set the camera's position to the target position with the same z component.
    self.transform.position = Vector3(fTargetX, fTargetY, self.transform.position.z)
end

-- Return this class.
return LgCameraFollow
