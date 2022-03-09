// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract TestContractDictionary {
    mapping(string => uint) collections;
    string[] keys;

    constructor() {
        collections["a"] = 1;
        collections["b"] = 2;
        collections["c"] = 3;

        keys.push("a");
        keys.push("b");
        keys.push("c");
    }

    function SearchKeyV1(string calldata keyToFind) external view returns(string memory, uint) {
        
        for (uint8 i=0; i < keys.length; i++) {
            if (StringsAreEquals(keys[i], keyToFind)) {
                uint value = collections[keyToFind];
                return (keyToFind, value);
            }
        }

        revert("Key not found");
    }

    function StringsAreEquals(string memory string1, string memory string2) private pure returns(bool) {
        return keccak256(abi.encodePacked(string1)) == keccak256(abi.encodePacked(string2));
    }
}