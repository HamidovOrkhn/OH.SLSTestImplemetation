// By Orxan Hamidov


(function (global) {

    function LoginSession(setCookieUrl, oauthLogoutUrl, oauthLoginUrl, clientBaseUrl, permId) {
        fetch(oauthLoginUrl + "/" + permId, { credentials: 'include', method: 'GET' })
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (data.status == "Authenticated" || data.status == "Refreshed") {
                    var payload = parseJwt(data.token);
                    $.get(`${setCookieUrl}?token=${data.token}&date=${payload.exp}`, function () {
                        console.log("api sess")
                        //window.location.reload();
                    })
                }
                else {
                    window.location.replace(oauthLogoutUrl + `?url=${clientBaseUrl}&permId=${permId}`);
                    return;
                }
            })
    }

    function RefreshSession(oauthLogoutUrl, oauthCheckUrl, clientBaseUrl, permId, token) {
        fetch(oauthCheckUrl + `/${permId}`, { credentials: 'include', method: 'GET', headers: { 'Authorization': `${token}` } })
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (data.status == "Authenticated" || data.status == "Refreshed") {
                    return;
                }
                else {
                    window.location.replace(oauthLogoutUrl + `?url=${clientBaseUrl}&permId=${permId}`);
                    return;
                }
            })
    }

    // Public Namespace
    global.OHGlobalLogin = {
        LoginSession: LoginSession,
        RefreshSession: RefreshSession
    };

})(this);