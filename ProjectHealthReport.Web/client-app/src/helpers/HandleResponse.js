import $ from "jquery";
import "./notify";
export function handleAxiosError(error) {
    // @ts-ignore
    $.notify(error?.message, "error");
    if (error.response.data && error.response.data.error) {
        // @ts-ignore
        $.notify(error.response.data.error, { autoHide: false, className: 'error' });
    }
    else {
        if (error.response.status === 400) {
            // @ts-ignore
            $.notify("Input values are incorrect", { autoHide: false, className: 'error' });
        }
        else {
            // @ts-ignore
            $.notify(error.response.data.title, { autoHide: false, className: 'error' });
        }
    }
}
export function notify(message, type) {
    // @ts-ignore
    $.notify(message, type);
}
//# sourceMappingURL=HandleResponse.js.map