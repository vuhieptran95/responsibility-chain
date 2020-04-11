﻿let $backlogItemsTable = $("#tableBacklogItems");

let latestRemainingItems = parseInt($backlogItemsTable.data("latestRemainingItems"));
let latestRemainingStoryPoints = parseInt($backlogItemsTable.data("latestRemainingStoryPoints"));

let $remainingItems = $("#tableBacklogItems .remainingItems");
let $remainingStoryPoints = $("#tableBacklogItems .remainingStoryPoints");

let $itemsAdded = $("#tableBacklogItems .itemsAdded");
let $storyPointsAdded = $("#tableBacklogItems .storyPointsAdded");
let $itemsDone = $("#tableBacklogItems .itemsDone");
let $storyPointsDone = $("#tableBacklogItems .storyPointsDone");

let backlogInputedEvent = "backlogInputted";

$backlogItemsTable.on(backlogInputedEvent, calculateRemaining);

$backlogItemsTable.on(backlogInputedEvent, calculateAverage);

$(document).ready(function () {
    
    $backlogItemsTable.trigger(backlogInputedEvent);
})

function inputBacklogChanged(el) {
    el.setAttribute("value", el.value);

    $backlogItemsTable.trigger(backlogInputedEvent);
}


function calculateRemaining() {
    let proxyItemsDone = 0;
    let proxyItemsAdded = 0;
    let proxyStoryPointsDone = 0;
    let proxyStoryPointAdded = 0;

    if ($itemsAdded.val()) {
        proxyItemsAdded = parseInt($itemsAdded.val())
    }
    if ($itemsDone.val()) {
        proxyItemsDone = parseInt($itemsDone.val())
    }
    if ($storyPointsAdded.val()) {
        proxyStoryPointAdded = parseInt($storyPointsAdded.val());
    }
    if ($storyPointsDone.val()) {
        proxyStoryPointsDone = parseInt($storyPointsDone.val());
    }

    let remainingItems = (latestRemainingItems) + (proxyItemsAdded) - (proxyItemsDone);

    let remainingStoryPoints = (latestRemainingStoryPoints) + (proxyStoryPointAdded) - (proxyStoryPointsDone)

    $remainingItems.html(remainingItems);
    $remainingStoryPoints.html(remainingStoryPoints);
}

let numberOfRow = parseInt($backlogItemsTable.data("numberOfRow")); 
let totalItemsAdded = parseInt($backlogItemsTable.data("totalItemsAdded"));
let totalItemsDone = parseInt($backlogItemsTable.data("totalItemsDone"));
let totalStoryPointsAdded = parseInt($backlogItemsTable.data("totalStoryPointsAdded"));
let totalStoryPointsDone = parseInt($backlogItemsTable.data("totalStoryPointsDone"));

let $avgItemsAdded = $("#tableBacklogItems .avgItemsAdded");
let $avgStoryPointsAdded = $("#tableBacklogItems .avgStoryPointsAdded");
let $avgItemsDone = $("#tableBacklogItems .avgItemsDone");
let $avgStoryPointsDone = $("#tableBacklogItems .avgStoryPointsDone");

function calculateAverage() {
    let proxyItemsDone = 0;
    let proxyItemsAdded = 0;
    let proxyStoryPointsDone = 0;
    let proxyStoryPointAdded = 0;

    if ($itemsAdded.val()) {
        proxyItemsAdded = parseInt($itemsAdded.val())
    }
    if ($itemsDone.val()) {
        proxyItemsDone = parseInt($itemsDone.val())
    }
    if ($storyPointsAdded.val()) {
        proxyStoryPointAdded = parseInt($storyPointsAdded.val());
    }
    if ($storyPointsDone.val()) {
        proxyStoryPointsDone = parseInt($storyPointsDone.val());
    }

    if (numberOfRow > 0) {
        let avgItemsAdded = totalItemsAdded / numberOfRow;
        if (proxyItemsAdded !== 0) {
            avgItemsAdded = (totalItemsAdded + proxyItemsAdded) / (numberOfRow + 1);
        }
        $avgItemsAdded.html(_.floor(avgItemsAdded,1));

        let avgStoryPointsAdded = totalStoryPointsAdded / numberOfRow;
        if (proxyStoryPointAdded !== 0) {
            avgStoryPointsAdded = (totalStoryPointsAdded + proxyStoryPointAdded) / (numberOfRow + 1);
        }
        $avgStoryPointsAdded.html(_.floor(avgStoryPointsAdded,1));

        let avgItemsDone = totalItemsDone / numberOfRow;
        if (proxyItemsDone !== 0) {
            avgItemsDone = (totalItemsDone + proxyItemsDone) / (numberOfRow + 1);
        }
        $avgItemsDone.html(_.floor(avgItemsDone,1));

        let avgStoryPointsDone = totalStoryPointsDone / numberOfRow;
        if (proxyStoryPointsDone !== 0) {
            avgStoryPointsDone = (totalStoryPointsDone + proxyStoryPointsDone) / (numberOfRow + 1);
        }
        $avgStoryPointsDone.html(_.floor(avgStoryPointsDone,1));
    }
}
