--
-- Enemy class.
--
-- @filename  LgBomb.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-03
--

local YwDeclare = YwDeclare
local YwClass = YwClass

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
local LgEnemy = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgEnemy.this = false

-- The transform.
LgEnemy.transform = false

-- The c# gameObject.
LgEnemy.gameObject = false

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
    self.m_cScore = GameObject.Find("Score"):GetComponent(Score)
end

-- Fixed update method.
function LgEnemy:FixedUpdate()
    --print("LgEnemy:FixedUpdate")

    local this = self.this

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
    self.gameObject:GetComponent(Rigidbody2D).velocity = Vector2(self.transform.localScale.x * this.m_moveSpeed, self.gameObject:GetComponent(Rigidbody2D).velocity.y)

    -- If the enemy has one hit point left and has a damagedEnemy sprite...
    if (1 == this.m_HP) and (not Slua.IsNull(this.m_damagedEnemy)) then
        -- ... set the sprite renderer's sprite to be the damagedEnemy sprite.
        self.m_cSpriteRen.sprite = this.m_damagedEnemy
    end

    -- If the enemy has zero or fewer hit points and isn't dead yet...
    if (this.m_HP <= 0) and (not self.m_bDead) then
        -- ... call the death function.
        self:Death()
    end
end

function LgEnemy:Death()
    --print("LgEnemy:Death")

    local this = self.this

    -- Find all of the sprite renderers on this object and it's children.
    local aOtherRenderers = self.gameObject:GetComponentsInChildren(SpriteRenderer)

    -- Disable all of them sprite renderers.
    for i = 1, aOtherRenderers.Length do
        aOtherRenderers[i].enabled = false
    end

    -- Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
    self.m_cSpriteRen.enabled = true
    self.m_cSpriteRen.sprite = this.m_deadEnemy

    -- Increase the score by 100 points.
    self.m_cScore.m_score = self.m_cScore.m_score + 100

    -- Set dead to true.
    self.m_bDead = true

    -- Allow the enemy to rotate and spin it by adding a torque.
    local cRigid2D = self.gameObject:GetComponent(Rigidbody2D)
    cRigid2D.constraints = RigidbodyConstraints2D.None
    cRigid2D:AddTorque(this.m_deathSpinMin + math.random() * (this.m_deathSpinMax - this.m_deathSpinMin))

    -- Find all of the colliders on the gameobject and set them all to be triggers.
    local aColls = self.gameObject:GetComponents(Collider2D)
    for i = 1, aColls.Length do
        aColls[i].isTrigger = true
    end

    -- Play a random audioclip from the deathClips array.
    local nIdx = math.random(1, #this.m_deathClips)
    AudioSource.PlayClipAtPoint(this.m_deathClips[nIdx], self.transform.position)

    -- Create a vector that is just above the enemy.
    local vScorePos = self.transform.position
    vScorePos.y = vScorePos.y + 1.5

    -- Instantiate the 100 points prefab at this point.
    GameObject.Instantiate(this.m_hundredPointsUI, vScorePos, Quaternion.identity)
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
