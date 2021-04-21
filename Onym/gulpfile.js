/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    less = require("gulp-less");

var paths = {
    static: "./Content/",
    webroot: "./wwwroot/"
};
gulp.task("less", function () {
    return gulp.src('Content/Less/*.less')
        .pipe(less())
        .pipe(gulp.dest(paths.static + '/css'))
});