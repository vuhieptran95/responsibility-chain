import moment from "moment";
import _ from "lodash";
const defaultReportViewSettings = {
    projectId: 0,
    year: 0,
    week: 0,
    numberOfWeek: 4,
    numberOfWeekNotShowClosedItems: 2
};
const VueHelper = {
    range: function (start, end) {
        let array = [];
        for (let i = start; i <= end; i++) {
            array.push(i);
        }
        return array;
    },
    formatMomentDate: function (dateString, formatString) {
        return moment(dateString).format(formatString);
    },
    getCurrentYearWeek: function () {
        return moment().isoWeekYear() * 100 + moment().isoWeek();
    },
    getLastYearWeek: function (yearweek) {
        let year = this.calculateYear(yearweek);
        let week = this.calculateWeek(yearweek);
        let weekString = "";
        if (week < 10)
            weekString = "0" + week;
        let time = moment(`${year}W${weekString}`).subtract(1, "week");
        return time.isoWeekYear() * 100 + time.isoWeek();
    },
    calculateYear: function (yearWeek) {
        return _.floor(yearWeek / 100, 0);
    },
    calculateWeek: function (yearWeek) {
        let year = _.floor(yearWeek / 100, 0);
        return yearWeek - year * 100;
    },
    getWorkingDays(yearWeek) {
        let year = this.calculateYear(yearWeek);
        let week = this.calculateWeek(yearWeek);
        let startDay = moment().isoWeekYear(year).isoWeek(week).isoWeekday("Monday");
        let endDay = moment().isoWeekYear(year).isoWeek(week).isoWeekday("Friday");
        return { startDay, endDay };
    },
    displayYearWeek: function (yearWeek) {
        let year = this.calculateYear(yearWeek);
        let week = this.calculateWeek(yearWeek);
        return `${year} - ${week}`;
    },
    formatYearWeek(yearWeek) {
        let year = this.calculateYear(yearWeek);
        let week = this.calculateWeek(yearWeek);
        return { text: `${week} - ${year}`, value: yearWeek };
    },
    createQueryStringDefault: function (projectId, yearweek) {
        let viewSettings = defaultReportViewSettings;
        viewSettings.projectId = projectId;
        viewSettings.year = this.calculateYear(yearweek);
        viewSettings.week = this.calculateWeek(yearweek);
        return `projectId=${viewSettings.projectId}&year=${viewSettings.year}&week=${viewSettings.week}&numberOfWeek=${viewSettings.numberOfWeek}&numberOfWeekNotShowClosedItem=${viewSettings.numberOfWeekNotShowClosedItems}`;
    },
    createQueryString: function (viewSettings) {
        return `projectId=${viewSettings.projectId}&year=${viewSettings.year}&week=${viewSettings.week}&numberOfWeek=${viewSettings.numberOfWeek}&numberOfWeekNotShowClosedItem=${viewSettings.numberOfWeekNotShowClosedItems}`;
    },
    formatCurrentStatus: function (status) {
        if (status === "missed")
            return { color: "red", text: "Missed", tooltip: status };
        else if (status === "filled")
            return { color: "grey", text: "Filled", tooltip: status };
        else if (status === "ontime")
            return { color: "green", text: "On Time", tooltip: status };
        else if (status === "notfill")
            return { color: "yellow", text: "Not Fill", tooltip: status };
        else
            return undefined;
    }
};
export default VueHelper;
//# sourceMappingURL=VueHelper.js.map