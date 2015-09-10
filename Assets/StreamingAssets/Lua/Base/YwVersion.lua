--
-- Lua version parse file.
--
-- @filename  Version.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-06-08
--

-- Get global declare.
local YwDeclare = require "Base/YwGlobal"

-- Get current version number.
local _, _, majorv, minorv, rev = string.find(_VERSION, "(%d).(%d)[.]?([%d]?)")
local VersionNumber = tonumber(majorv) * 100 + tonumber(minorv) * 10 + (((string.len(rev) == 0) and 0) or tonumber(rev))

-- Declare current version number.
YwDeclare("YW_VERSION", VersionNumber)

-- Declare lua history version number.
YwDeclare("YW_VERSION_510", 510)
YwDeclare("YW_VERSION_520", 520)
YwDeclare("YW_VERSION_530", 530)

return YW_VERSION
