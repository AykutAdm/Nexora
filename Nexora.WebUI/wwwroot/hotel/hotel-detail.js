/**
 * Nexora — hotel detail static page only
 */
"use strict";

(function () {
  var HOTEL_IMAGES = [
    "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=600&q=80",
    "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=600&q=80",
    "https://images.unsplash.com/photo-1582719508461-905c673771fd?w=600&q=80",
    "https://images.unsplash.com/photo-1571896349842-33c89424de2d?w=600&q=80",
  ];
  var HOTEL_NAMES = [
    "The Grand Pera Palace",
    "Bosphorus Suites Hotel",
    "Çırağan Prestige",
    "Galata Tower Residences",
    "The Marmara Istanbul",
  ];
  var DESCS = [
    "Overlooking the shimmering Bosphorus Strait, this iconic property blends Ottoman grandeur with contemporary luxury.",
    "A refined sanctuary in the heart of the city, offering panoramic views and impeccable five-star service.",
  ];
  var GALLERY_IMAGES = [
    "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=1400&q=85",
    "https://images.unsplash.com/photo-1582719508461-905c673771fd?w=1400&q=85",
    "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=1400&q=85",
  ];
  var AMENITIES = [
    { icon: "fa-wifi", label: "Free WiFi" },
    { icon: "fa-swimming-pool", label: "Rooftop Pool" },
    { icon: "fa-spa", label: "Luxury Spa" },
    { icon: "fa-utensils", label: "Fine Dining" },
  ];

  function generateHotels(count) {
    var i,
      list = [];
    for (i = 0; i < count; i++) {
      list.push({
        id: i + 1,
        name: HOTEL_NAMES[i % HOTEL_NAMES.length],
        stars: [3, 4, 4, 5, 5][i % 5],
        rating: (8 + Math.random() * 1.8).toFixed(1),
        price: Math.floor(Math.random() * 400 + 80),
        address: "Beyoğlu, Istanbul, Turkey",
        lat: (41.0082 + (Math.random() - 0.5) * 0.3).toFixed(4),
        lng: (28.9784 + (Math.random() - 0.5) * 0.3).toFixed(4),
        image: HOTEL_IMAGES[i % HOTEL_IMAGES.length],
        gallery: GALLERY_IMAGES,
        description: DESCS[i % DESCS.length],
        checkinTime: "14:00",
        checkoutTime: "12:00",
        amenities: AMENITIES,
      });
    }
    return list;
  }

  function renderStars(count, max) {
    max = max || 5;
    var i,
      html = "";
    for (i = 0; i < max; i++) {
      html +=
        '<i class="fa-' +
        (i < count ? "solid" : "regular") +
        " fa-star star-icon" +
        (i >= count ? " empty" : "") +
        '"></i>';
    }
    return html;
  }

  function initGallery(images) {
    var track = document.getElementById("galleryTrack");
    var dotsEl = document.getElementById("galleryDots");
    var counter = document.getElementById("galleryCounter");
    var currentSlide = 0;
    if (!track) return;

    track.innerHTML = images
      .map(function (src) {
        return '<div class="gallery-slide"><img src="' + src + '" alt="" loading="lazy" /></div>';
      })
      .join("");

    dotsEl.innerHTML = images
      .map(function (_, i) {
        return '<div class="gallery-dot' + (i === 0 ? " active" : "") + '" data-index="' + i + '"></div>';
      })
      .join("");

    function goTo(i) {
      currentSlide = (i + images.length) % images.length;
      track.style.transform = "translateX(-" + currentSlide * 100 + "%)";
      document.querySelectorAll(".gallery-dot").forEach(function (d, di) {
        d.classList.toggle("active", di === currentSlide);
      });
      if (counter) counter.textContent = currentSlide + 1 + " / " + images.length;
    }

    document.getElementById("galleryPrev").addEventListener("click", function () {
      goTo(currentSlide - 1);
    });
    document.getElementById("galleryNext").addEventListener("click", function () {
      goTo(currentSlide + 1);
    });
    dotsEl.querySelectorAll(".gallery-dot").forEach(function (d, i) {
      d.addEventListener("click", function () {
        goTo(i);
      });
    });
    setInterval(function () {
      goTo(currentSlide + 1);
    }, 5000);
  }

  function initBookingCalc(pricePerNight) {
    function calcTotal() {
      var cin = document.getElementById("bookCheckin").value;
      var cout = document.getElementById("bookCheckout").value;
      if (!cin || !cout) return;
      var nights = Math.max(1, (new Date(cout) - new Date(cin)) / 86400000);
      var subtotal = nights * pricePerNight;
      var tax = subtotal * 0.12;
      function setEl(id, val) {
        var el = document.getElementById(id);
        if (el) el.textContent = val;
      }
      setEl("nightsLabel", nights + " night" + (nights > 1 ? "s" : "") + " × $" + pricePerNight);
      setEl("nightsTotal", "$" + subtotal.toFixed(0));
      setEl("taxTotal", "$" + tax.toFixed(0));
      setEl("grandTotal", "$" + (subtotal + tax).toFixed(0));
    }
    var tomorrow = new Date(Date.now() + 86400000).toISOString().split("T")[0];
    var checkout = new Date(Date.now() + 86400000 * 4).toISOString().split("T")[0];
    var binCin = document.getElementById("bookCheckin");
    var binCout = document.getElementById("bookCheckout");
    if (binCin) binCin.value = tomorrow;
    if (binCout) binCout.value = checkout;
    binCin.addEventListener("change", calcTotal);
    binCout.addEventListener("change", calcTotal);
    calcTotal();
  }

  function renderSimilar(hotels, currentId) {
    var grid = document.getElementById("similarGrid");
    if (!grid) return;
    var similar = hotels.filter(function (h) {
      return h.id !== currentId;
    }).slice(0, 4);
    grid.innerHTML = similar
      .map(function (h) {
        var starsSmall = "";
        var s;
        for (s = 0; s < h.stars; s++) starsSmall += '<i class="fa-solid fa-star star-icon"></i>';
        return (
          '<div class="hotel-card" style="cursor:pointer" onclick="localStorage.setItem(\'nexora-hotel-id\',' +
          h.id +
          ');location.reload()"><div class="hotel-card-img-wrap"><img class="hotel-card-img" src="' +
          h.image +
          '" alt="" loading="lazy" /></div><div class="hotel-card-body"><div class="hotel-card-stars">' +
          starsSmall +
          '</div><h3 class="hotel-card-name">' +
          h.name +
          '</h3><div class="hotel-card-footer"><div class="hotel-price"><strong>$' +
          h.price +
          '</strong><span>/night</span></div></div></div></div>'
        );
      })
      .join("");
  }

  function initDetail() {
    if (!document.querySelector(".detail-page")) return;

    var id = parseInt(localStorage.getItem("nexora-hotel-id") || "1", 10);
    var hotels = JSON.parse(localStorage.getItem("nexora-hotels") || "null") || generateHotels(20);
    var hotel = hotels.find(function (h) {
      return h.id === id;
    }) || hotels[0];

    initGallery(hotel.gallery);

    function setId(id, val) {
      var el = document.getElementById(id);
      if (el) el.textContent = val;
    }

    document.getElementById("detailStars").innerHTML = hotel.stars ? renderStars(hotel.stars) : "";
    setId("detailHotelName", hotel.name);
    setId("detailAddress", hotel.address);
    setId("detailCoords", hotel.lat + "°N, " + hotel.lng + "°E");
    setId("checkinTime", hotel.checkinTime);
    setId("checkoutTime", hotel.checkoutTime);
    setId("detailRating", hotel.rating);
    setId("detailPrice", "$" + hotel.price);
    setId("detailDescription", hotel.description);
    setId("mapAddressOverlay", "📍 " + hotel.address + " · " + hotel.lat + "°N, " + hotel.lng + "°E");
    setId("sidebarPrice", "$" + hotel.price);
    document.getElementById("sidebarStars").innerHTML = renderStars(hotel.stars);

    var amenGrid = document.getElementById("amenitiesGrid");
    if (amenGrid) {
      amenGrid.innerHTML = hotel.amenities
        .map(function (a) {
          return '<div class="amenity-item"><i class="fa-solid ' + a.icon + '"></i>' + a.label + "</div>";
        })
        .join("");
    }

    document.title = hotel.name + " — Nexora";
    initBookingCalc(hotel.price);
    renderSimilar(hotels, hotel.id);
  }

  document.addEventListener("DOMContentLoaded", initDetail);
})();
