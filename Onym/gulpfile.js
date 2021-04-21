/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    less = require("gulp-less");

var paths = {
    content: "./Content/",
    webroot: "./wwwroot/"
};
gulp.task("less", function () {
    return gulp.src('Content/Less/*.less')
        .pipe(less())
        .pipe(gulp.dest(paths.content + '/css'))
});