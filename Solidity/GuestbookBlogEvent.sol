// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract TestContractEvent {
    string[] private _messages;
    event MessageInserted(uint counter, string message);

    function SendMessage(string calldata message) external {
        _messages.push(message);
        emit MessageInserted(_messages.length + 1, message);
    }

    function GetMessages() external view returns(string[] memory) {
        return _messages;
    }
}
