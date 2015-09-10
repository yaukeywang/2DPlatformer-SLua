--
-- Player control class.
--
-- @filename  LgPlayerControl.lua
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

-- Register new class LgPlayerControl.
local strClassName = "LgPlayerControl"
local LgPlayerControl = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgPlayerControl.this = false

-- The transform.
LgPlayerControl.transform = false

-- The c# gameObject.
LgPlayerControl.gameObject = false

-- The index of the taunts array indicating the most recent taunt.
LgPlayerControl.m_nTauntIndex = 0

-- A position marking where to check if the player is grounded. (Transform)
LgPlayerControl.m_cGroundCheck = false

-- Whether or not the player is grounded.
LgPlayerControl.m_bGrounded = false

-- Reference to the player's animator component.
LgPlayerControl.m_cAnim = false

-- The destroy flag.
LgPlayerControl.m_bDestroy = false

-- Awake method.
function LgPlayerControl:Awake()
    --print("LgPlayerControl:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgPlayerControl!")
        return
    end

    -- Setting up references.
    self.m_cGroundCheck = self.transform:Find("groundCheck")
    self.m_cAnim = self.gameObject:GetComponent(Animator)
end

-- Update method.
function LgPlayerControl:Update()
    --print("LgPlayerControl:Update")

    -- Check the validation.
    if self.m_bDestroy then
        return
    end

    local this = self.this

    -- The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
    local cHitRes = Physics2D.Linecast(self.transform.position, self.m_cGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
    self.m_bGrounded = not Slua.IsNull(cHitRes.collider)

    -- If the jump button is pressed and the player is grounded then the player should jump.
    if Input.GetButtonDown("Jump") and self.m_bGrounded then
        this.m_bJump = true
    end
end

-- Fixed update method.
function LgPlayerControl:FixedUpdate()
    --print("LgPlayerControl:FixedUpdate")

    -- Check the validation.
    if self.m_bDestroy then
        return
    end

    local this = self.this

    -- Cache the horizontal input.
    local fHoriz = Input.GetAxis("Horizontal")

    -- The Speed animator parameter is set to the absolute value of the horizontal input.
    self.m_cAnim:SetFloat("Speed", math.abs(fHoriz))

    -- If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
    local cRigid = self.gameObject:GetComponent(Rigidbody2D)
    if fHoriz * cRigid.velocity.x < this.m_maxSpeed then
        cRigid:AddForce(Vector2.right * fHoriz * this.m_moveForce)
    end

    -- If the player's horizontal velocity is greater than the maxSpeed...
    if math.abs(cRigid.velocity.x) > this.m_maxSpeed then
        -- ... set the player's velocity to the maxSpeed in the x axis.
        cRigid.velocity = Vector2(Mathf.Sign(cRigid.velocity.x) * this.m_maxSpeed, cRigid.velocity.y)
    end

    -- If the input is moving the player right and the player is facing left...
    if (fHoriz > 0.0) and (not this.m_bFacingRight) then
        -- ... flip the player.
        self:Flip()
    -- Otherwise if the input is moving the player left and the player is facing right...
    elseif (fHoriz < 0.0) and this.m_bFacingRight then
        -- ... flip the player.
        self:Flip()
    end

    -- If the player should jump...
    if this.m_bJump then
        -- Set the Jump animator trigger parameter.
        self.m_cAnim:SetTrigger("Jump")

        -- Play a random jump audio clip.
        local nIdx = Random.Range(1, #this.m_jumpClips + 1)
        AudioSource.PlayClipAtPoint(this.m_jumpClips[nIdx], self.transform.position);

        -- Add a vertical force to the player.
        cRigid:AddForce(Vector2(0.0, this.m_jumpForce))

        -- Make sure the player can't jump again until the jump conditions from Update are satisfied.
        this.m_bJump = false
    end
end

-- On destroy method.
function LgPlayerControl:OnDestroy()
    --print("LgPlayerControl:OnDestroy")
    self.m_bDestroy = true
end

function LgPlayerControl:Flip()
    --print("LgPlayerControl:Flip")

    local this = self.this

    -- Switch the way the player is labelled as facing.
    this.m_bFacingRight = not this.m_bFacingRight

    -- Multiply the player's x local scale by -1.
    local vTheScale = self.transform.localScale
    vTheScale.x = vTheScale.x * -1.0
    self.transform.localScale = vTheScale
end

function LgPlayerControl:Taunt()
    --print("LgPlayerControl:Taunt")

    -- Check the validation.
    if self.m_bDestroy then
        return
    end

    local this = self.this

    -- Create a coroutine.
    local cCor = coroutine.create(function ()
        -- Check the random chance of taunting.
        local fTauntChance = math.random() * 100.0
        if fTauntChance > this.m_tauntProbability then
            -- Wait for tauntDelay number of seconds.
            Yield(WaitForSeconds(this.m_tauntDelay))

            -- Check the validation.
            if self.m_bDestroy then
                return
            end

            -- If there is no clip currently playing.
            local cAs = self.gameObject:GetComponent(AudioSource)
            if not cAs.isPlaying then
                -- Choose a random, but different taunt.
                self.m_nTauntIndex = self:TauntRandom()

                -- Play the new taunt.
                cAs.clip = this.m_taunts[self.m_nTauntIndex]
                cAs:Play()
            end
        end
    end)

    coroutine.resume(cCor)
end

function LgPlayerControl:TauntRandom()
    --print("LgPlayerControl:TauntRandom")

    -- Choose a random index of the taunts array.
    local nIdx = Random.Range(1, #self.this.m_taunts + 1)

    -- If it's the same as the previous taunt...
    if nIdx == self.m_nTauntIndex then
        -- ... try another random taunt.
        return self:TauntRandom()
    else
        -- Otherwise return this index.
        return nIdx
    end
end

-- Return this class.
return LgPlayerControl
