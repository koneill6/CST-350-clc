let timer = {
    // HTML Elements.
    el_tens: null,
    el_secs: null,
    el_mins: null,
    // Interval
    tens: 00,
    secs: 00,
    mins: 00,
    interval: null,
    start: function() {
        this.reset();
        this.interval = setInterval(this.tick.bind(this), 10);
    },
    stop: function() {
        clearInterval(this.interval);
        this.interval = null;
    },
    reset: function () {
        this.stop();

        this.tens = 00;
        this.secs = 00;
        this.mins = 00;
    },
    tick: function() {
        this.tens++;
        if (this.tens <= 9) {
            this.el_tens.innerHTML = "0" + this.tens;
        } else {
            this.el_tens.innerHTML = this.tens;
        }

        if (this.tens > 99) {
            this.secs++;

            this.el_secs.innerHTML = "0" + this.secs;
            this.tens = 0;

            this.el_tens.innerHTML = "0" + this.tens;
        }

        if (this.secs > 9) {
            this.el_secs.innerHTML = this.secs;
        }

        if (this.secs > 59) {
            this.mins++;

            this.el_mins.innerHTML = "0" + this.mins;
            this.secs = 0;

            this.el_secs.innerHTML = "0" + this.secs;
            this.tens = 0;

            this.el_tens.innerHTML = "0" + this.tens;
        }
    },
    fix: function() {
        this.el_tens.innerHTML = this._format(this.tens);
        this.el_secs.innerHTML = this._format(this.secs);
        this.el_mins.innerHTML = this._format(this.mins);
    },
    _format: function(n) {
        return n < 10 ? `0${n}` : n;
    }
};

window.$ms = timer;