/// <binding Clean='clean' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglifyCss = require('gulp-uglifycss'),
    del = require('del'),
    templateCache = require('gulp-angular-templatecache')

gulp.task('clean', function () {
    return del(['wwwroot/lib'])
});

gulp.task('copyJavaScript', function () {
    
    gulp.src(['bower_components/jquery/dist/jquery.min.js',
              'bower_components/bootstrap/dist/js/bootstrap.min.js',
              'scripts/angular.min.js',
              'scripts/angular-local-storage.min.js',
              'scripts/angular-route.min.js',
              'scripts/angular-tooltips.min.js',
              'scripts/loading-bar.min.js',
              'scripts/modernizr-2.6.2-respond-1.1.0.min.js'])
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest('wwwroot/lib'));

    gulp.src(['scripts/app/app.js',
    'scripts/app/controllers/*.js',
    'scripts/app/services/*.js'])
    .pipe(concat('parichaylite.js'))
    .pipe(gulp.dest('wwwroot/lib'));

    gulp.src(['scripts/app/views/*.html',
    'scripts/app/views/*.htm',
    'scripts/app/views/**/*.html',
    'scripts/app/views/**/*.htm'])
    .pipe(templateCache('templates.js'))
    .pipe(gulp.dest('wwwroot/lib'));

});

gulp.task('copyCss', function () {

    gulp.src(['bower_components/bootstrap/dist/css/bootstrap.min.css', 'wwwroot/blog.css'])
        .pipe(uglifyCss())
        .pipe(concat('site.css'))
        .pipe(gulp.dest('wwwroot/lib'))

});

gulp.task('copyHtml', function () {

    gulp.src(['scripts/app/views/**.*'])
        .pipe(gulp.dest('wwwroot/lib/scripts/app/views'))

});

gulp.task('default', ['clean', 'copyJavaScript', 'copyCss', 'copyHtml']);