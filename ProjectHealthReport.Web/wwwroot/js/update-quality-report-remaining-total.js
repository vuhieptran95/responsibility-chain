let $qualityReportTable = $("#tableQualityReport");

let $criticalBug = $("#tableQualityReport .criticalBug");
let $majorBug = $("#tableQualityReport .majorBug");
let $minorBug = $("#tableQualityReport .minorBug");
let $doneBug = $("#tableQualityReport .doneBug");
let $reOpenBug = $("#tableQualityReport .reOpenBug");

let latestRemainingBugs = parseInt($qualityReportTable.data("latestRemainingBugs"));

let $totalBugs = $("#tableQualityReport .totalBugs");
let $remainingBugs = $("#tableQualityReport .remainingBugs");

let qualityReportChangedEvent = "QualityReportChanged";


$qualityReportTable.on(qualityReportChangedEvent, calculateTotalRemaining);


$(document).ready(function () {
    $qualityReportTable.trigger(qualityReportChangedEvent);
})

function inputQualityChanged(el) {
    el.setAttribute("value", el.value);

    $qualityReportTable.trigger(qualityReportChangedEvent);
}

function calculateTotalRemaining() {
    let criticalBug = 0;
    let majorBug = 0;
    let minorBug = 0;
    let doneBug = 0;
    let reOpenBug = 0; 

    if ($criticalBug.val()) {
        criticalBug = parseInt($criticalBug.val());
    }
    if ($majorBug.val()) {
        majorBug = parseInt($majorBug.val());
    }
    if ($minorBug.val()) {
        minorBug = parseInt($minorBug.val());
    }
    if ($doneBug.val()) {
        doneBug = parseInt($doneBug.val());
    }
    if ($reOpenBug.val()) {
        reOpenBug = parseInt($reOpenBug.val());
    }

    let totalBugs = criticalBug + majorBug + minorBug;
    let remainingBugs = latestRemainingBugs + totalBugs - doneBug;

    $totalBugs.html(totalBugs);
    $remainingBugs.val(remainingBugs);
}

