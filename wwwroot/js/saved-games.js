// Utilize AJAX call to retrieve saved games by user ID
function getSavedGamesByUserId() {
    const userId = $('#user-id').val();

    $.ajax({
        url: `api/savedGames/user/${userId}`,
        type: 'GET',
        dataType: 'json',
        success: onSuccess,
        error: onError
    });
}

// Helper for ajax success.
function onSuccess(data, status, xhr) {
    _logDebug('xhr', '/UserLanding', true, data, status, xhr);

    // Retrieve the table body by id.
    let tbody = document.getElementById("save-game-table-body");

    // Create and populated the page where all the saved boards will be
    for (let i = 0; i < data.length; i++) {
        console.log(data[i]);

        let row = document.createElement('tr');
        row.className = "sg_table";

        let id = document.createElement('td');
        id.innerText = data[i].id;

        let date = document.createElement('td');
        date.innerText = data[i].save_date;

        let buttons = document.createElement('td');
        buttons.className = "d-flex justify-content-around btn_api_style"

        // Retrieve Previous Game Board - AJAX call
        let playButton = document.createElement('button');
        playButton.innerText = "Play";
        playButton.className = "btn gb_btn_style_api"
        playButton.onclick = function () {
            $.ajax({
                url: `api/savedGames/${data[i].id}/load`,
                type: 'GET',
                contentType: 'application/x-www-form-urlencoded',
                success: (data, status, xhr) => {
                    window.location = `Game/Index/${1}`;
                },
                error: onError
            });
        }

        // Delete board AJAX call
        let deleteButton = document.createElement('button');
        deleteButton.innerText = "Delete";
        deleteButton.className = "btn btn-danger"
        deleteButton.onclick = function () {
            $.ajax({
                url: `api/savedGames/${data[i].id}`,
                type: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: (data, status, xhr) => {
                    window.location.reload();
                },
                error: onError
            });
        }

        // Append all created child elements to the table

        buttons.appendChild(playButton);
        buttons.appendChild(deleteButton);

        let hidden = document.createElement('td');
        hidden.innerText = data[i].save_state;

        row.appendChild(id);
        row.appendChild(date);
        row.appendChild(buttons);
        row.appendChild(hidden);

        tbody.appendChild(row);
    }
}

// Helper for ajax error.
function onError(status, err) {
    _logDebug('xhr', '/UserLanding', false, status, err);
    alert(status);
}

// Debug printing.
function _logDebug(source, path, didSucceed, ...payload) {
    console.info(`[${source} ${didSucceed ? 'success' : 'error'}] @ ${path}`, payload);
}

getSavedGamesByUserId();