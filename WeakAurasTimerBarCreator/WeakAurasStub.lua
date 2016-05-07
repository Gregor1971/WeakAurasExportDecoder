WeakAuras = {}

local loadstring = load

local myFunc = assert(loadstring("local i = 5; i = i + 1"))()

local encode_func = assert(loadstring("return function(self, str) return str:gsub(self.encode_search, self.encode_translate); end"))()

local Compresser = LibStub:GetLibrary("LibCompress");
local Encoder = Compresser:GetAddonEncodeTable()
--[[
local Serializer = LibStub:GetLibrary("AceSerializer-3.0");


function TableToString(inTable, forChat)
    local serialized = Serializer:Serialize(inTable);
    local compressed = Compresser:CompressHuffman(serialized);
    if(forChat) then
        return encodeB64(compressed);
    else
        return Encoder:Encode(compressed);
    end
end

function StringToTable(inString, fromChat)
    local decoded;
    if(fromChat) then
        decoded = decodeB64(inString);
    else
        decoded = Encoder:Decode(inString);
    end
    local decompressed, errorMsg = Compresser:Decompress(decoded);
    if not(decompressed) then
        return "Error decompressing: "..errorMsg;
    end
    local success, deserialized = Serializer:Deserialize(decompressed);
    if not(success) then
        return "Error deserializing "..deserialized;
    end
    return deserialized;
end

]]--
