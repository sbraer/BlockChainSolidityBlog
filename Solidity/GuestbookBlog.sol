// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract TestContract {
    string[] private _messages;

    function SendMessage(string calldata message) external {
        _messages.push(message);
    }

    function GetMessages() external view returns(string[] memory) {
        return _messages;
    }
}
