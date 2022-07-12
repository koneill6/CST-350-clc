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

    // TODO: jQuery get the table body by id.
    let tbody = document.getElementById("save-game-table-body");

    for (let i = 0; i < data.length; i++) {
        // TODO: Append each data row as an element into the table body. (save_state is hidden so we can access it later)
        // id, save_date, save_state, user_id
        console.log(data[i]);

        // TODO: When adding buttons, register event handlers for their respective ajax calls.
        // DELETE -> DELETE /savedGames/{id}. Where id is the data[i].id
        //  onSuccess -> window.location.reload()

        let row = document.createElement('tr');
        row.className = "sg_table";

        let id = document.createElement('td');
        id.innerText = data[i].id;

        let date = document.createElement('td');
        date.innerText = data[i].save_date;

        let buttons = document.createElement('td');
        buttons.className = "d-flex justify-content-around btn_api_style"

        // TODO: Assign id.
        // TODO: Bind event handler for click.
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

        // TODO: Assign id.
        // TODO: Bind event handler for click.
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