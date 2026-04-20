/**
 * Hotel detail — hero image carousel (scroll-snap + peek)
 */
(function () {
  "use strict";

  function init() {
    var sc = document.getElementById("detailGalleryScroll");
    if (!sc) return;

    var slides = sc.querySelectorAll(".detail-hero-slide");
    var n = slides.length;
    if (!n) return;

    var prev = document.getElementById("detailGalleryPrev");
    var next = document.getElementById("detailGalleryNext");
    var dotsWrap = document.getElementById("detailGalleryDots");
    var counter = document.getElementById("detailGalleryCounter");

    var idx = 0;
    var scrollEndTimer;

    function centerOfSlide(el) {
      return el.offsetLeft + el.offsetWidth / 2;
    }

    function nearestIndex() {
      var mid = sc.scrollLeft + sc.clientWidth / 2;
      var best = 0;
      var bestDist = Infinity;
      for (var i = 0; i < slides.length; i++) {
        var c = centerOfSlide(slides[i]);
        var d = Math.abs(c - mid);
        if (d < bestDist) {
          bestDist = d;
          best = i;
        }
      }
      return best;
    }

    function scrollToIndex(i, smooth) {
      i = Math.max(0, Math.min(n - 1, i));
      idx = i;
      var slide = slides[i];
      var target = slide.offsetLeft - (sc.clientWidth - slide.offsetWidth) / 2;
      sc.scrollTo({ left: Math.max(0, target), behavior: smooth ? "smooth" : "auto" });
      syncUi();
    }

    function syncUi() {
      if (counter) counter.textContent = idx + 1 + " / " + n;
      if (dotsWrap) {
        var dots = dotsWrap.querySelectorAll(".gallery-dot");
        for (var d = 0; d < dots.length; d++) {
          dots[d].classList.toggle("active", d === idx);
          dots[d].setAttribute("aria-current", d === idx ? "true" : "false");
        }
      }
      if (prev) prev.setAttribute("aria-disabled", n <= 1 ? "true" : "false");
      if (next) next.setAttribute("aria-disabled", n <= 1 ? "true" : "false");
    }

    function onScrollEnd() {
      idx = nearestIndex();
      syncUi();
    }

    sc.addEventListener(
      "scroll",
      function () {
        clearTimeout(scrollEndTimer);
        scrollEndTimer = setTimeout(onScrollEnd, 80);
      },
      { passive: true }
    );

    if (prev)
      prev.addEventListener("click", function () {
        scrollToIndex(idx - 1 < 0 ? n - 1 : idx - 1, true);
      });
    if (next)
      next.addEventListener("click", function () {
        scrollToIndex(idx + 1 >= n ? 0 : idx + 1, true);
      });

    if (dotsWrap) {
      dotsWrap.querySelectorAll(".gallery-dot").forEach(function (dot, i) {
        dot.addEventListener("click", function () {
          scrollToIndex(i, true);
        });
      });
    }

    window.addEventListener("resize", function () {
      scrollToIndex(idx, false);
    });

    window.addEventListener("load", function () {
      scrollToIndex(idx, false);
    });

    if (typeof ResizeObserver !== "undefined") {
      var ro = new ResizeObserver(function () {
        scrollToIndex(idx, false);
      });
      ro.observe(sc);
    }

    slides.forEach(function (slide) {
      var img = slide.querySelector("img");
      if (img && !img.complete) {
        img.addEventListener("load", function onImgLoad() {
          img.removeEventListener("load", onImgLoad);
          scrollToIndex(idx, false);
        });
      }
    });

    sc.addEventListener("keydown", function (e) {
      if (e.key === "ArrowLeft") {
        e.preventDefault();
        scrollToIndex(idx - 1 < 0 ? n - 1 : idx - 1, true);
      } else if (e.key === "ArrowRight") {
        e.preventDefault();
        scrollToIndex(idx + 1 >= n ? 0 : idx + 1, true);
      }
    });

    requestAnimationFrame(function () {
      scrollToIndex(0, false);
      requestAnimationFrame(function () {
        scrollToIndex(idx, false);
      });
    });
  }

  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", init);
  else init();
})();
