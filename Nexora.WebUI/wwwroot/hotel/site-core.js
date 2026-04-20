/**
 * Nexora — shared UI (all pages)
 */
"use strict";

(function () {
  function initTheme() {
    var html = document.documentElement;
    var btn = document.getElementById("themeToggle");
    var icon = document.getElementById("themeIcon");
    function apply(theme) {
      html.setAttribute("data-theme", theme);
      localStorage.setItem("nexora-theme", theme);
      if (icon) icon.className = theme === "dark" ? "fa-solid fa-moon" : "fa-solid fa-sun";
    }
    apply(localStorage.getItem("nexora-theme") || "dark");
    if (btn) {
      btn.addEventListener("click", function () {
        apply(html.getAttribute("data-theme") === "dark" ? "light" : "dark");
      });
    }
  }

  function initNav() {
    var nav = document.getElementById("navbar");
    if (!nav) return;
    function onScroll() {
      nav.classList.toggle("scrolled", window.scrollY > 30);
    }
    window.addEventListener("scroll", onScroll, { passive: true });
    onScroll();
  }

  function initScrollAnim() {
    var els = document.querySelectorAll(".fade-in-up, .dash-card");
    if (!els.length) return;
    var observer = new IntersectionObserver(
      function (entries) {
        entries.forEach(function (entry) {
          if (entry.isIntersecting) {
            entry.target.classList.add("visible");
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.12, rootMargin: "0px 0px -40px 0px" }
    );
    els.forEach(function (el) {
      observer.observe(el);
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    initTheme();
    initNav();
    initScrollAnim();
  });
})();
