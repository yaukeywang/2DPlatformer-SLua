--
-- The camera follow class.
--
-- @filename  LgCameraFollow.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgCameraFollow.
local strClassName = "LgCameraFollow"
local LgCameraFollow = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- Distance in the x axis the player can move before the camera follows.
LgCameraFollow.m_fXMargin = 1.0;

-- Distance in the y axis the player can move before the camera follows.
LgCameraFollow.m_fYMargin = 1.0;

-- How smoothly the camera catches up with it's target movement in the x axis.
LgCameraFollow.m_fXSmooth = 8.0;

-- How smoothly the camera catches up with it's target movement in the y axis.
LgCameraFollow.m_fYSmooth = 8.0;

-- The maximum x and y coordinates the camera can have.
LgCameraFollow.m_fMaxX = 0.0;
LgCameraFollow.m_fMaxY = 0.0;

-- The minimum x and y coordinates the camera can have.
LgCameraFollow.m_fMinX = 0.0;
LgCameraFollow.m_fMinY = 0.0;

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

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    local aFloatArray = cDataBridge.m_floats

    -- Set move params.
    self.m_fXMargin = aFloatArray[1]
    self.m_fYMargin = aFloatArray[2]
    self.m_fXSmooth = aFloatArray[3]
    self.m_fYSmooth = aFloatArray[4]
    self.m_fMaxX = aFloatArray[5]
    self.m_fMaxY = aFloatArray[6]
    self.m_fMinX = aFloatArray[7]
    self.m_fMinY = aFloatArray[8]
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
    return math.abs(self.transform.position.x - self.m_cTrsPlayer.position.x) > self.m_fXMargin
end

-- Check y margin.
function LgCameraFollow:CheckYMargin()
    --print("LgCameraFollow:CheckYMargin")
    
    -- Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
    return math.abs(self.transform.position.y - self.m_cTrsPlayer.position.y) > self.m_fYMargin
end

-- Track player.
function LgCameraFollow:TrackPlayer()
    --print("LgCameraFollow:TrackPlayer")
    
    -- By default the target x and y coordinates of the camera are it's current x and y coordinates.
    local fTargetX = self.transform.position.x
    local fTargetY = self.transform.position.y

    -- If the player has moved beyond the x margin...
    if self:CheckXMargin() then
        -- ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
        fTargetX = Mathf.Lerp(self.transform.position.x, self.m_cTrsPlayer.position.x, self.m_fXSmooth * Time.deltaTime)
    end

    -- If the player has moved beyond the y margin...
    if self:CheckYMargin() then
        -- ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
        fTargetY = Mathf.Lerp(self.transform.position.y, self.m_cTrsPlayer.position.y, self.m_fYSmooth * Time.deltaTime)
    end

    -- The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
    -- Parameter match type error.
    --fTargetX = Mathf.Clamp(fTargetX, self.m_fMinX, self.m_fMaxX)
    --fTargetY = Mathf.Clamp(fTargetY, self.m_fMinY, self.m_fMaxY)

    if fTargetX < self.m_fMinX then
        fTargetX = self.m_fMinX
    elseif fTargetX > self.m_fMaxX then
        fTargetX = self.m_fMaxX
    end

    if fTargetY < self.m_fMinY then
        fTargetY = self.m_fMinY
    elseif fTargetY > self.m_fMaxY then
        fTargetY = self.m_fMaxY
    end

    -- Set the camera's position to the target position with the same z component.
    self.transform.position = Vector3(fTargetX, fTargetY, self.transform.position.z)
end

-- Return this class.
return LgCameraFollow
