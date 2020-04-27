import $ from "jquery"
import "./notify"
import axios from "axios";
import {AUTH_ENDPOINT} from "@/helpers/EndPoint";

export function handleAxiosError(error: any) {
    if (error.response.status === 401) {
        console.log(error.response);
        // @ts-ignore
        $.notify(error.message, "error");

        if (error.response.data.redirectUri) {
            setTimeout(function () {
                window.location.href = error.response.data.redirectUri;
            }, 1300);
        }else{
            let base64 = btoa(window.location.href);
            axios.get(`${AUTH_ENDPOINT}?redirectUrlIdP=${base64}`).catch(e => {
                if (e.response.data.redirectUri) {
                    setTimeout(function () {
                        window.location.href = e.response.data.redirectUri;
                    }, 1300);
                }
            })
        }


        return;
    }
    // @ts-ignore
    $.notify(error.message, "error");
    if (error.response.data.error) {
        // @ts-ignore
        $.notify(error.response.data.error, {autoHide: false, className: 'error'});
    } else {
        if (error.response.status === 400) {
            // @ts-ignore
            $.notify("Input values are incorrect", {autoHide: false, className: 'error'});
        } else {
            // @ts-ignore
            $.notify(error.response.data.title, {autoHide: false, className: 'error'});
        }
    }
}

export function notify(message: string, type: string) {
    // @ts-ignore
    $.notify(message, type)
}

export function notifyWithOption(message: string, option: any) {
    // @ts-ignore
    $.notify(message, option)
}