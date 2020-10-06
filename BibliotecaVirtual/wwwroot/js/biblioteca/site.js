// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var site = {};
site.url = location.protocol + "//" + location.host + "/";

/** Congela o objeto para prevenir manipulação. */
Object.freeze(site);