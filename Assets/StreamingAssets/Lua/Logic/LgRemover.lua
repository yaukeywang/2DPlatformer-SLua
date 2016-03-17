--
-- The remover class.
--
-- @filename  LgRemover.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgRemover.
local strClassName = "LgRemover"
local LgRemover = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The splash object.
LgRemover.m_cSplash = nil

-- Awake method.
function LgRemover:Awake()
    --print("LgRemover:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgRemover!")
        return
    end

    -- Get splash object.
    self.m_cSplash = self.m_aParameters[1]
end

-- OnTriggerEnter2D method.
function LgRemover:OnTriggerEnter2D(cOtherCollider2D)
    --print("LgRemover:OnTriggerEnter2D")

    local this = self.this

    -- If the player hits the trigger...
    if "Player" == cOtherCollider2D.gameObject.tag then
        -- .. stop the camera tracking the player
        GameObject.FindGameObjectWithTag("MainCamera"):GetComponent(YwLuaMonoBehaviour).enabled = false

        -- .. stop the Health Bar following the player.
        local cHealthBarObj = GameObject.FindGameObjectWithTag("HealthBar")
        if cHealthBarObj.activeSelf then
            cHealthBarObj:SetActive(false)
        end

        -- ... instantiate the splash where the player falls in.
        GameObject.Instantiate(self.m_cSplash, cOtherCollider2D.transform.position, self.transform.rotation)

        -- ... destroy the player.
        GameObject.Destroy(cOtherCollider2D.gameObject)

        -- ... reload the level.
        self:ReloadGame()
    else
        -- ... instantiate the splash where the enemy falls in.
        GameObject.Instantiate(self.m_cSplash, cOtherCollider2D.transform.position, self.transform.rotation)

        -- Destroy the enemy.
        GameObject.Destroy(cOtherCollider2D.gameObject)
    end
end

function LgRemover:ReloadGame()
    --print("LgRemover:ReloadGame")

    local cCol = coroutine.create(function ()
        -- ... pause briefly.
        Yield(WaitForSeconds(2.0))

        -- ... and then reload the level.
        SceneManagement.SceneManager.LoadScene("Level")
    end)

    coroutine.resume(cCol)
end

-- Return this class.
return LgRemover
