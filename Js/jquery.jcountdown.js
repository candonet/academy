var createDigits = function (a, b) {
    for (var d, e, c = 0, f = 0; f < b.startTime.length; f++) {
        if (parseInt(b.startTime[f]) >= 0) {
            if (elem = $('<div id="cnt_' + c + '" class="cntDigit" />').css({height: b.digitHeight, "float": "left", background: "url('" + b.image + "')", width: b.digitWidth}), elem.current = parseInt(b.startTime[f]), digits.push(elem), margin(c, -elem.current * b.digitHeight * b.digitImages), b.continuous === !0)digits[c]._max = function () {
                return 9
            }; else switch (b.format[f]) {
                case"h":
                    digits[c]._max = function (a, b) {
                        return 0 == a % 2 ? 2 : b ? 3 : 9
                    };
                    break;
                case"d":
                    digits[c]._max = function () {
                        return 9
                    };
                    break;
                case"m":
                    digits[c]._max = function (a) {
                        return d || (d = a), a == d ? 9 : 5
                    };
                    break;
                case"s":
                    digits[c]._max = function (a) {
                        return e || (e = a), a == e ? 9 : 5
                    }
            }
            c += 1
        } else elem = $('<div class="cntSeparator"/>').css({"float": "left"}).text(b.startTime[f]);
        a.append(elem)
    }
}, makeMovement = function (a, b, c, d) {
    intervals[a] && window.clearInterval(intervals[a]);
    var e = -(d.digitHeight * d.digitImages * digits[a].current);
    margin(a, e), digits[a].current = digits[a].current + (c ? b : -b);
    var f = 0;
    intervals[a] = setInterval(function () {
        if (f++ === d.digitImages * b)return window.clearInterval(intervals[a]), delete intervals[a], void 0;
        var g = c ? -d.digitHeight : d.digitHeight;
        margin(a, e + f * g)
    }, d.stepTime / b)
}, margin = function (a, b) {
    return void 0 !== b ? (digits[a].margin = b, digits[a].css({backgroundPosition: "0 " + b + "px"})) : digits[a].margin || 0
}, moveDigit = function (a, b) {
    if (0 != digits[a].current)makeMovement(a, 1, !1, b); else if (a > 0) {
        var c = 0 == digits[a - 1].current;
        makeMovement(a, digits[a]._max(a, c), !0, b), moveDigit(a - 1, b)
    } else {
        for (var d = 0; d < digits.length; d++)clearInterval(intervals[d]), clearInterval(intervals.main), margin(d, 0);
        b.timerEnd()
    }
}, parseRelativeDate = function (a, b) {
    var h, i, c = new Date, d = c.getDate(), e = c.getMonth() + 1, f = c.getFullYear(), g = c.getHours(), j = b.format, k = a.split(":");
    return 0 == j.indexOf("dd") && (d = k[0], k = k.slice(1), j = j.substr(3)), 0 == j.indexOf("hh") && (g = k[0], k = k.slice(1), j = j.substr(3)), 0 == j.indexOf("mm") && (h = k[0], k = k.slice(1), j = j.substr(3)), 0 == j.indexOf("ss") && (i = k[0], k = k.slice(1), j = j.substr(3)), new Date([e, d, f].join("/") + " " + [g, h, i].map(pad).join(":") + " GMT-0900")
}, formatCompute = function (a, b) {
    var c = b.format, d = {d: a.getUTCDate() - 1, h: a.getUTCHours(), m: a.getUTCMinutes(), s: a.getUTCSeconds()};
    return c.replace(/(dd|hh|mm|ss)/g, function (a, b) {
        return pad(d[b[0]])
    })
}, pad = function (a) {
    return(1e15 + "" + a).substr(-2)
}, digits = [], intervals = [];
jQuery.fn.countdown = function (a) {
    var b = {stepTime: 60, format: "dd:hh:mm:ss", startTime: "01:12:32:55", digitImages: 6, digitWidth: 67, digitHeight: 90, timerEnd: function () {
    }, image: "digits.png", continuous: !1};
    if ($.extend(b, a), a.endTime) {
        var c = a.endTime instanceof Date ? a.endTime : parseRelativeDate(a.endTime, b), d = c.getTime() - (new Date).getTime();
        a.startTime = formatCompute(new Date(d), b), delete a.endTime
    }
    $.extend(b, a), createDigits(this, b), intervals.main = setInterval(function () {
        moveDigit(digits.length - 1, b)
    }, 1e3)
};