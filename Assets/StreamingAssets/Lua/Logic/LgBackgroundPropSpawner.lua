--
-- LgBackgroundPropSpawner class.
--
-- @filename  LgBackgroundPropSpawner.lua
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

-- Register new class LgBackgroundPropSpawner.
local strClassName = "LgBackgroundPropSpawner"
local LgBackgroundPropSpawner = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgBackgroundPropSpawner.this = false

-- The transform.
LgBackgroundPropSpawner.transform = false

-- The c# gameObject.
LgBackgroundPropSpawner.gameObject = false

-- Public.

-- The prop to be instantiated.
LgBackgroundPropSpawner.m_cBackgroundProp = false

-- The x coordinate of position if it's instantiated on the left.
LgBackgroundPropSpawner.m_fLeftSpawnPosX = 0.1

-- The x coordinate of position if it's instantiated on the right.
LgBackgroundPropSpawner.m_fRightSpawnPosX = 1.0

-- The lowest possible y coordinate of position.
LgBackgroundPropSpawner.m_fMinSpawnPosY = 0.1

-- The highest possible y coordinate of position.
LgBackgroundPropSpawner.m_fMaxSpawnPosY = 1.0

-- The shortest possible time between spawns.
LgBackgroundPropSpawner.m_fMinTimeBetweenSpawns = 0.1

-- The longest possible time between spawns.
LgBackgroundPropSpawner.m_fMaxTimeBetweenSpawns = 0.5

-- The lowest possible speed of the prop.
LgBackgroundPropSpawner.m_fMinSpeed = 0.1

-- The highest possible speeed of the prop.
LgBackgroundPropSpawner.m_fMaxSpeed = 0.1

-- Pirvate.

-- The destroyed flag.
LgBackgroundPropSpawner.m_bDestroy = false

-- Awake method.
function LgBackgroundPropSpawner:Awake()
    --print("LgBackgroundPropSpawner:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgBackgroundPropSpawner!")
        return
    end

    -- Init the destroy flag.
    self.m_bDestroy = false
end

-- Start method.
function LgBackgroundPropSpawner:Start()
    --print("LgBackgroundPropSpawner:Start")

    -- Set the random seed so it's not the same each game.
    math.randomseed(os.time())

    -- Start the spawn coroutine.
    self:Spawn()
end

-- On destroy method.
function LgBackgroundPropSpawner:OnDestroy()
    --print("LgBackgroundPropSpawner:OnDestroy")

    -- Set the destroy flag to true.
    self.m_bDestroy = true
end

-- The spawn method.
function LgBackgroundPropSpawner:Spawn()
    --print("LgBackgroundPropSpawner:Spawn")

    -- Return if destroyed.
    if self.m_bDestroy then
        return
    end

    local cCor = coroutine.create(function ()
        -- Create a random wait time before the prop is instantiated.
        local fWaitTime = self.m_fMinTimeBetweenSpawns + math.random() * (self.m_fMaxTimeBetweenSpawns - self.m_fMinTimeBetweenSpawns)

        -- Wait for the designated period.
        Yield(WaitForSeconds(fWaitTime))

        -- Skip if already destroyed.
        if self.m_bDestroy then
            return
        end

        -- Randomly decide whether the prop should face left or right.
        local bFacingLeft = math.random(0, 2) == 0

        -- If the prop is facing left, it should start on the right hand side, otherwise it should start on the left.
        local fPosX = (bFacingLeft and self.m_fRightSpawnPosX) or self.m_fLeftSpawnPosX

        -- Create a random y coordinate for the prop.
        local fPosY = self.m_fMinSpawnPosY + math.random() * (self.m_fMaxSpawnPosY - self.m_fMinSpawnPosY)

        -- Set the position the prop should spawn at.
        local vSpawnPos = Vector3(fPosX, fPosY, self.transform.position.z)

        -- Instantiate the prop at the desired position.
        local cPropInstance = GameObject.Instantiate(self.m_cBackgroundProp, vSpawnPos, Quaternion.identity)

        -- The sprites for the props all face left.  Therefore, if the prop should be facing right...
        if not bFacingLeft then
            -- ... flip the scale in the x axis.
            local vScale = cPropInstance.transform.localScale
            vScale.x = vScale.x * -1.0
            cPropInstance.transform.localScale = vScale
        end

        -- Create a random speed.
        local fSpeed = self.m_fMinSpeed + math.random() * (self.m_fMaxSpeed - self.m_fMinSpeed)

        -- These speeds would naturally move the prop right, so if it's facing left, multiply the speed by -1.
        fSpeed = fSpeed * ((bFacingLeft and -1.0) or 1.0)

        -- Set the prop's velocity to this speed in the x axis.
        cPropInstance.velocity = Vector2(fSpeed, 0.0)

        -- Restart the coroutine to spawn another prop.
        self:Spawn()

        -- While the prop exists...
        while not Slua.IsNull(cPropInstance) do
            -- ... and if it's facing left...
            if bFacingLeft then
                -- ... and if it's beyond the left spawn position...
                if cPropInstance.transform.position.x < self.m_fLeftSpawnPosX - 0.5 then
                    -- ... destroy the prop.
                    GameObject.Destroy(cPropInstance.gameObject)
                end
            else
                -- Otherwise, if the prop is facing right and it's beyond the right spawn position...
                if cPropInstance.transform.position.x > self.m_fRightSpawnPosX + 0.5 then
                    -- ... destroy the prop.
                    GameObject.Destroy(cPropInstance.gameObject)
                end
            end

            -- Return to this point after the next update.
            Yield(0)

            -- Skip if already destroyed.
            if self.m_bDestroy then
                return
            end
        end
    end)

    coroutine.resume(cCor)
end

-- Return this class.
return LgBackgroundPropSpawner
