--
-- Enemy class.
--
-- @filename  LgBomb.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-03
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

local math = math
local Physics2D = Physics2D
local Vector2 = Vector2
local Vector3 = Vector3
local Quaternion = Quaternion

-- Register new class LgEnemy.
local strClassName = "LgEnemy"
local LgEnemy = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The speed the enemy moves at.
LgEnemy.m_fMoveSpeed = 2.0

-- How many times the enemy can be hit before it dies.
LgEnemy.m_nHP = 2

-- A value to give the minimum amount of Torque when dying.
LgEnemy.m_fDeathSpinMin = -100.0

-- A value to give the maximum amount of Torque when dying.
LgEnemy.m_fDeathSpinMax = 100.0

-- A sprite of the enemy when it's dead.
LgEnemy.m_cDeadEnemy = nil

-- An optional sprite of the enemy when it's damaged.
LgEnemy.m_cDamagedEnemy = nil

-- An array of audioclips that can play when the enemy dies.
LgEnemy.m_aDeathClips = nil

-- A prefab of 100 that appears when the enemy dies.
LgEnemy.m_cHundredPointsUI = nil;

-- Private.

-- Reference to the sprite renderer.
LgEnemy.m_cSpriteRen = false

-- Reference to the position of the gameobject used for checking if something is in front.
LgEnemy.m_cTrsFrontCheck = false

-- Whether or not the enemy is dead.
LgEnemy.m_bDead = false

-- Reference to the Score script.
LgEnemy.m_cScore = false

-- Awake method.
function LgEnemy:Awake()
    --print("LgEnemy:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgEnemy!")
        return
    end

    -- Setting up the references.
    self.m_cSpriteRen = self.transform:Find("body"):GetComponent(SpriteRenderer)
    self.m_cTrsFrontCheck = self.transform:Find("frontCheck").transform
    self.m_cScore = GameObject.Find("Score"):GetComponent(YwLuaMonoBehaviour):GetLuaTable()

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    local aFloatArray = cDataBridge.m_floats
    local aSpriteArray = cDataBridge.m_sprites
    local aAudioArray = cDataBridge.m_audioClips

    -- Set params.
    self.m_fMoveSpeed = aFloatArray[1]
    self.m_nHP = math.tointeger(aFloatArray[2])
    self.m_fDeathSpinMin = aFloatArray[3]
    self.m_fDeathSpinMax = aFloatArray[4]

    self.m_cDeadEnemy = aSpriteArray[1]
    self.m_cDamagedEnemy = aSpriteArray[2]

    self.m_aDeathClips = {}
    for i = 1, #aAudioArray do
        self.m_aDeathClips[i] = aAudioArray[i]
    end

    self.m_cHundredPointsUI = self.m_aParameters[1]
end

-- Fixed update method.
function LgEnemy:FixedUpdate()
    --print("LgEnemy:FixedUpdate")

    -- Create an array of all the colliders in front of the enemy.
    local aFrontHits = Physics2D.OverlapPointAll(self.m_cTrsFrontCheck.position, 1)

    -- Check each of the colliders.
    for i = 1, aFrontHits.Length do
        -- If any of the colliders is an Obstacle...
        if "Obstacle" == aFrontHits[i].tag then
            -- ... Flip the enemy and stop checking the other colliders.
            self:Flip()
            break
        end
    end

    -- Set the enemy's velocity to moveSpeed in the x direction.
    self.gameObject:GetComponent(Rigidbody2D).velocity = Vector2(self.transform.localScale.x * self.m_fMoveSpeed, self.gameObject:GetComponent(Rigidbody2D).velocity.y)

    -- If the enemy has one hit point left and has a damagedEnemy sprite...
    if (1 == self.m_nHP) and (not Slua.IsNull(self.m_cDamagedEnemy)) then
        -- ... set the sprite renderer's sprite to be the damagedEnemy sprite.
        self.m_cSpriteRen.sprite = self.m_cDamagedEnemy
    end

    -- If the enemy has zero or fewer hit points and isn't dead yet...
    if (self.m_nHP <= 0) and (not self.m_bDead) then
        -- ... call the death function.
        self:Death()
    end
end

function LgEnemy:Hurt()
    --print("LgEnemy:Hurt")
    self.m_nHP = self.m_nHP - 1
end

function LgEnemy:Death()
    --print("LgEnemy:Death")

    -- Find all of the sprite renderers on this object and it's children.
    local aOtherRenderers = self.gameObject:GetComponentsInChildren(SpriteRenderer)

    -- Disable all of them sprite renderers.
    for i = 1, aOtherRenderers.Length do
        aOtherRenderers[i].enabled = false
    end

    -- Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
    self.m_cSpriteRen.enabled = true
    self.m_cSpriteRen.sprite = self.m_cDeadEnemy

    -- Increase the score by 100 points.
    self.m_cScore.m_nScore = self.m_cScore.m_nScore + 100

    -- Set dead to true.
    self.m_bDead = true

    -- Allow the enemy to rotate and spin it by adding a torque.
    local cRigid2D = self.gameObject:GetComponent(Rigidbody2D)
    cRigid2D.constraints = RigidbodyConstraints2D.None
    cRigid2D:AddTorque(self.m_fDeathSpinMin + math.random() * (self.m_fDeathSpinMax - self.m_fDeathSpinMin))

    -- Find all of the colliders on the gameobject and set them all to be triggers.
    local aColls = self.gameObject:GetComponents(Collider2D)
    for i = 1, aColls.Length do
        aColls[i].isTrigger = true
    end

    -- Play a random audioclip from the deathClips array.
    local nIdx = math.random(1, #self.m_aDeathClips)
    AudioSource.PlayClipAtPoint(self.m_aDeathClips[nIdx], self.transform.position)

    -- Create a vector that is just above the enemy.
    local vScorePos = self.transform.position
    vScorePos.y = vScorePos.y + 1.5

    -- Instantiate the 100 points prefab at this point.
    GameObject.Instantiate(self.m_cHundredPointsUI, vScorePos, Quaternion.identity)
end

function LgEnemy:Flip()
    --print("LgEnemy:Flip")

    -- Multiply the x component of localScale by -1.
    local vEnemyScale = self.transform.localScale
    vEnemyScale.x = vEnemyScale.x * -1.0
    self.transform.localScale = vEnemyScale
end

-- Return this class.
return LgEnemy
