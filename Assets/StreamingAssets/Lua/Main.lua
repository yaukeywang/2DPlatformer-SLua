--
-- Lua main entry file.
--
-- @filename  Main.lua
-- @copyright Copyright (c) 2015 Yaukey all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-20
--

-- Get the lua version.
print(jit and "jit on, using: " .. jit.version .. " " .. jit.arch or "jit off, using: " .. _VERSION)

-- Load essential.
require "Logic/LgSetupFiles"

-- To create manager.
local function CreateManager()
    --print("Main:CreateManager")
    
    -- Create dispatcher and mono behaviour updater first.
    YwDispatcher.Instance():Validate()
    YwMonoBehaviourUpdater.Instance():Validate()

    -- Validate each manager.
end

-- To init manager.
local function InitManager()
    --print("Main:InitManager")

    -- Init manager.
end

-- To release manager.
local function ReleaseManager()
    --print("Main:ReleaseManager")

    -- Release manager.
end

-- Begin to run game.
local function RunGame()
    --print("Main:RunGame")

    -- Enter into login scene.
    print("Game is running...")
    SceneManagement.SceneManager.LoadScene("Level")
    print("Scene \"Level\" is loaded ok!")
end

-- The global main function.
local function main()
    --print("Main:main")

    -- Create manager.
    CreateManager()

    -- Init all manager.
    InitManager()

    -- Run game.
    RunGame()
end

-- The main logic update entry.
local function Update()
    --print("Main:Update")

    -- Run the main update entry.
    YwDispatcher.Instance():Update()

    -- Run the mono behaviour update.
    YwMonoBehaviourUpdater.Instance():Update()
end

-- The main logic late update entry.
local function LateUpdate()
    --print("Main:LateUpdate")

    -- Run the mono behaviour late update.
    YwMonoBehaviourUpdater.Instance():LateUpdate()
end

-- The main logic fixed update entry.
local function FixedUpdate()
    --print("Main:FixedUpdate")

    -- Run the mono behaviour fixed update.
    YwMonoBehaviourUpdater.Instance():FixedUpdate()
end

-- The main logic lite update entry.
local function LiteUpdate()
    --print("Main:LiteUpdates")

    -- Run the mono behaviour lite update.
    YwMonoBehaviourUpdater.Instance():LiteUpdate()
end

-- Declare global function.
YwDeclare("main", main)
YwDeclare("Update", Update)
YwDeclare("LateUpdate", LateUpdate)
YwDeclare("FixedUpdate", FixedUpdate)
YwDeclare("LiteUpdate", LiteUpdate)

return main
