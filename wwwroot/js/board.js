// Register the Button Events
function registerButtonHandlers(sessionId) {
    for (let i = 0; i < 10; i++) {
        for (let j = 0; j < 10; j++) {
            let btn = $(`#ms-${i}-${j}`);

            // Cells
            btn.mousedown((ev) => {
                switch (ev.which) {
                    case 1:
                        // Left Click (Reveal)
                        revealCell(sessionId, i, j);
                        break;
                    case 3:
                        // Right Click (Flag)
                        flagCell(sessionId, i, j);
                        break;
                    default:
                    // Middle Mouse/Anything Else 
                }
            });

            // Prevent right click menu.
            btn.contextmenu(() => false);
        }
    }

    // Reset Button Handler
    $('#ms-reset').click((ev) => deleteBoard(sessionId));

    // Save Button Handler
    $('#ms-save').click((ev) => saveGame());
}

// Get the GameBoard partial.
function getGameBoard(sessionId) {
    $.ajax({
        url: '/Game/GetBoard',
        type: 'GET',
        data: {
            sessionId,
        },
        dataType: 'html',
        contentType: 'application/json',
        success: onSuccess,
        error: onError
    });
}

// Reveal a GameBoard cell.
function revealCell(sessionId, row, col) {
    $.ajax({
        url: '/Game/RevealBoard',
        type: 'GET',
        data: {
            sessionId,
            row,
            col
        },
        dataType: 'html',
        contentType: 'application/json',
        success: (data, status, xhr) => {
            if (!didFirstClick) {
                didFirstClick = true;
                $ms.start();
            }

            onSuccess(data, status, xhr);
        },
        error: onError
    });
}

// Flag a GameBoard cell.
function flagCell(sessionId, row, col) {
    $.ajax({
        url: '/Game/FlagBoard',
        type: 'GET',
        data: {
            sessionId,
            row,
            col
        },
        dataType: 'html',
        contentType: 'application/json',
        success: onSuccess,
        error: onError
    });
}

// Delete the board starting a new game.
function deleteBoard(sessionId) {
    $.ajax({
        url: '/Game/ResetBoard',
        type: 'GET',
        data: {
            sessionId
        },
        dataType: 'html',
        contentType: 'application/json',
        success: (data, status, xhr) => {
            // We need to navigate back to index to trigger a new game. Reload works fine here.
            window.location.reload();
        },
        error: onError
    });
}

function saveGame() {
    $.ajax({
        url: '/api/savedGames',
        type: 'POST',
        data: {
            payload: $("#board-serialized").val(),
            userId: $("#user-id").val()
        },
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        success: (data, status, xhr) => {
            _logDebug('api-xhr', '/api/savedGames', true, data, status, xhr);
            window.location = `/UserLanding?username=${$("#user-name").val()}`;
        },
        error: onError
    });
}

// Helper for ajax success.
function onSuccess(data, status, xhr) {
    _logDebug('xhr', '/Game/GetBoard', true, data, status, xhr);
    $('#game-board').html(data);
    registerButtonHandlers(sessionId);
    bindTimer();
}

// Helper for ajax error.
function onError(status, err) {
    _logDebug('xhr', '/Game/GetBoard', false, status, err);
    alert(status);
}

// Debug printing.
function _logDebug(source, path, didSucceed, ...payload) {
    console.info(`[${source} ${didSucceed ? 'success' : 'error'}] @ ${path}`, payload);
}

function bindTimer() {
    $ms.el_tens = $('#tens')[0];
    $ms.el_secs = $('#second')[0];
    $ms.el_mins = $('#min')[0];

    // Force update to fix timer zeros.
    $ms.fix();
}

// Set session, get board.
const sessionId = $("#session-id").val();
let didFirstClick = false;

getGameBoard(sessionId);