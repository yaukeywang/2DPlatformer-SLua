--
-- Lua main entry file.
--
-- @filename  Main.lua
-- @copyright Copyright (c) 2015 Yaukey all rights reserved
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-20
--

-- Get the lua version.
print(jit and "jit on, using: " .. jit.version .. " " .. jit.arch or "jit off, using: " .. _VERSION)

-- Load essential.
require "Base/YwSetupFiles"

-- To init manager.
local function InitManager()
    -- Validate dispatcher.
    YwDispatcher.Instance():Validate()
end

-- Do test.
local function RunGame()
    -- Enter into login scene.
    print("Game is running...")
    SceneManagement.SceneManager.LoadScene("Level")
    print("Scene \"Level\" is loaded ok!")
end

-- The global main function.
local function main()
    -- Init all manager.
    InitManager()

    -- Do test.
    RunGame()
end

local function update()
    YwDispatcher.Instance():Update()
end

-- Declare global function.
local YwDeclare = YwDeclare
YwDeclare("main", main)
YwDeclare("update", update)

return main
