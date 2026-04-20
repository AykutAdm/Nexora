/**
 * Nexora — home / dashboard only (Index + static index.html)
 */
"use strict";

(function () {
  function initParticles() {
    var container = document.getElementById("heroParticles");
    if (!container) return;
    var i, p, size;
    for (i = 0; i < 18; i++) {
      p = document.createElement("div");
      p.className = "particle";
      size = Math.random() * 3 + 1;
      p.style.cssText =
        "width:" +
        size +
        "px;height:" +
        size +
        "px;left:" +
        Math.random() * 100 +
        "%;animation-duration:" +
        (Math.random() * 12 + 10) +
        "s;animation-delay:" +
        Math.random() * -15 +
        "s;opacity:" +
        (Math.random() * 0.5 + 0.1);
      container.appendChild(p);
    }
  }

  var dashboardData = {
    fuel: [
      { type: "Gasoline", unit: "RON95", price: "42.70", pct: 0.75 },
      { type: "Diesel", unit: "Euro 5", price: "40.10", pct: 0.68 },
      { type: "LPG", unit: "Autogas", price: "18.50", pct: 0.38 },
    ],
  };

  function initDashboard() {
    var el = document.getElementById("fuelList");
    if (el) {
      el.innerHTML = dashboardData.fuel
        .map(function (f) {
          return (
            '<div class="fuel-row"><div><div class="fuel-type">' +
            f.type +
            '</div><div class="fuel-unit">' +
            f.unit +
            '</div></div><div class="fuel-bar-wrap"><div class="fuel-bar" style="width:' +
            f.pct * 100 +
            '%"></div></div><div class="fuel-price-val">₺' +
            f.price +
            "</div></div>"
          );
        })
        .join("");
    }
  }

  /** Date min/value when inputs are empty (static index.html); MVC sets values server-side. */
  function initSearchFormHelpers() {
    var checkin = document.getElementById("checkin");
    var checkout = document.getElementById("checkout");
    if (!checkin || !checkout) return;

    if (!checkin.value) {
      var t = new Date();
      var today = t.toISOString().split("T")[0];
      var tomorrow = new Date(t.getTime() + 86400000).toISOString().split("T")[0];
      var plus3 = new Date(t.getTime() + 86400000 * 3).toISOString().split("T")[0];
      checkin.min = today;
      checkin.value = tomorrow;
      checkout.min = tomorrow;
      checkout.value = plus3;
    } else {
      checkout.min = checkin.value;
    }

    checkin.addEventListener("change", function () {
      checkout.min = checkin.value;
      if (checkout.value < checkin.value) checkout.value = checkin.value;
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    initParticles();
    initSearchFormHelpers();
    initDashboard();
  });
})();
