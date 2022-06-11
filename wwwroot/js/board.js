// Get the GameBoard partial.
function getGameBoard(sessionId) {
    $.ajax({
        url: '/Game/GetBoard',
        data: {
            sessionId,
        },
        contentType: 'application/html',
        type: 'GET',
        dataType: 'html',
        success: (data, status, xhr) => {
            _logDebug('xhr', '/Game/GetBoard', true, data, status, xhr);
            $('#game-board').html(data);
        },
        error: (status, err) => {
            _logDebug('xhr', '/Game/GetBoard', false, status, err);
            alert(status);
        }
    });
}

function _logDebug(source, path, didSucceed, ...payload) {
    console.info(`[${source} ${didSucceed ? 'success' : 'error'}] @ ${path}`, payload);
}

// TODO: Register event listeners.


// Exec.
const sessionId = $("#session-id").val();
getGameBoard(sessionId);