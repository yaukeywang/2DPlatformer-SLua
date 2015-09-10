--
-- Lua global definition file.
--
-- @filename  YwGlobal.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-28
--

-- The log error function.
local LogError = YwDebug.LogError

-- The declared global name.
local DeclaredNames = {}

-- The internal global variable table check method.
local function __inner_declare(Name, InitValue)
    if not rawget(_G, Name) then
        rawset(_G, Name, InitValue or false)
    else
        error("You have already declared global : " .. Name, 2)
    end
    
    DeclaredNames[Name] = true
    return _G[Name]
end

-- The internal global variable table index method.
local function __inner_declare_index(t, k)
    if not DeclaredNames[k] then
        error("Attempt to access an undeclared global variable : " .. k, 2)
    end

    return nil
end

-- The internal global variable table newindex method.
local function __inner_declare_newindex(t, k, v)
    if not DeclaredNames[k] then
        error("Attempt to write an undeclared global variable : " .. k, 2)
    else
        rawset(t, k, v)
    end
end

-- The global variable declare function.
local function __Declare(Name, InitValue)
    local ok, res = pcall(__inner_declare, Name, InitValue)
    if not ok then
        LogError(debug.traceback(res, 2))
        return nil
    else
        return res
    end
end

-- Check if a global variable is declared.
local function __IsDeclared(Name)
    if DeclaredNames[Name] or rawget(_G, Name) then
        return true
    else
        return false
    end
end

-- Set "Declare" into global.
if (not __IsDeclared("YwDeclare")) or (not YwDeclare) then
    __Declare("YwDeclare", __Declare)
end

-- Set "IsDeclared" into global.
if (not __IsDeclared("YwIsDeclared")) or (not YwIsDeclared) then
    __Declare("YwIsDeclared", __IsDeclared)
end

-- Set limit to define global variables.
setmetatable(_G,
{
    __index = function (t, k)
        local ok, res = pcall(__inner_declare_index, t, k)
        if not ok then
            LogError(debug.traceback(res, 2))
        end

        return nil
    end,

    __newindex = function (t, k, v)
        local ok, res = pcall(__inner_declare_newindex, t, k, v)
        if not ok then
            LogError(debug.traceback(res, 2))
        end
    end
})

-- Return this.
return __Declare
