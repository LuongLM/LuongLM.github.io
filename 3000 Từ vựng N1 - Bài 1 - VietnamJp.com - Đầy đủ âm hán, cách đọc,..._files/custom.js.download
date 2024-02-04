jQuery(document).ready(function($) {

    if(freshwp_ajax_object.sticky_menu){
    // grab the initial top offset of the navigation 
    var freshwpstickyNavTop = $('.freshwp-menu-outer-container').offset().top;
    
    // our function that decides weather the navigation bar should have "fixed" css position or not.
    var freshwpstickyNav = function(){
        var freshwpscrollTop = $(window).scrollTop(); // our current vertical position from the top
             
        // if we've scrolled more than the navigation, change its position to fixed to stick to top,
        // otherwise change it back to relative
        if (freshwpscrollTop > freshwpstickyNavTop) { 
            $('.freshwp-menu-outer-container').addClass('freshwp-fixed');
        } else {
            $('.freshwp-menu-outer-container').removeClass('freshwp-fixed'); 
        }
    };

    freshwpstickyNav();
    // and run it again every time you scroll
    $(window).scroll(function() {
        freshwpstickyNav();
    });
    }

    // $(".freshwp-nav-primary .freshwp-nav-menu").addClass("responsive-menu").before('<div class="freshwp-responsive-menu-icon"></div>');
    //$(".freshwp-nav-primary").before('<div class="freshwp-responsive-menu-icon">Menu</div>');
    
    $(".freshwp-nav-primary .freshwp-nav-menu").addClass("responsive-menu");

    $(".button_menu").click(function(){
        $(".freshwp-nav-primary").slideToggle();
        $(".my_box-menu").toggleClass("my_active");
    });

//search button
    $(".button_search").click(function(){
        $(".my_form_popup").toggleClass("my_hidden");
        if ($(".button_search .fa.fa-search")[0]){
            $(".button_search .fa").removeClass("fa-search");
            $(".button_search .fa").addClass("fa-close");
        } else{
            $(".button_search .fa").addClass("fa-search");
            $(".button_search .fa").removeClass("fa-close");            
        }

    });

// su-spoiler shortcode
    $(".su-spoiler").click(function(){
        // alert("The paragraph was clicked.");
        // $(".my_box-menu").toggleClass("my_active");
        $(this).toggleClass("su-spoiler-closed");
    });

// tabs plugin
    $('ul.nav.nav-tabs.my_tabs > li').click(function(){
        // alert("The paragraph was clicked.");
        var tab_id = $(this).attr('data-tab');

        $('ul.nav.nav-tabs.my_tabs > li').removeClass('active');
        $('.tab-content > .tab-pane').removeClass('active');

        $(this).addClass('active');
        $("#"+tab_id).addClass('active');
    })
// 

    $(window).resize(function(){
        if(window.innerWidth > 1076) {
            $(".freshwp-nav-primary .freshwp-nav-menu, nav .sub-menu, nav .children").removeAttr("style");
            $(".responsive-menu > li").removeClass("menu-open");
        }
    });

    $(".responsive-menu > li").click(function(event){
        if (event.target !== this)
        return;
        $(this).find(".sub-menu:first").slideToggle(function() {
            $(this).parent().toggleClass("menu-open");
        });
        $(this).find(".children:first").slideToggle(function() {
            $(this).parent().toggleClass("menu-open");
        });
    });

    $("div.freshwp-responsive-menu > ul > li").click(function(event) {
        if (event.target !== this)
            return;
        $(this).find("ul:first").slideToggle(function() {
            $(this).parent().toggleClass("menu-open");
        });
    });

    $(".post").fitVids();

    $( 'body' ).prepend( '<div class="freshwp-scroll-top"></div>');
    var scrollButtonEl = $( '.freshwp-scroll-top' );
    scrollButtonEl.hide();

    $( window ).scroll( function () {
        if ( $( window ).scrollTop() < 20 ) {
            $( '.freshwp-scroll-top' ).fadeOut();
        } else {
            $( '.freshwp-scroll-top' ).fadeIn();
        }
    } );

    scrollButtonEl.click( function () {
        $( "html, body" ).animate( { scrollTop: 0 }, 300 );
        return false;
    } );

    if ( $().owlCarousel ) {
        var freshwpcarouselwrapper = $('.freshwp-posts-carousel');
        var imgLoad = imagesLoaded(freshwpcarouselwrapper);
        imgLoad.on( 'always', function() {
            freshwpcarouselwrapper.each(function(){
                    var $this = $(this);
                    $this.find('.owl-carousel').owlCarousel({
                        autoplay: true,
                        loop: true,
                        margin: 10,
                        smartSpeed: 250,
                        dots: false,
                        nav: true,
                        autoplayTimeout: 4000,
                        autoHeight: true,
                        navText: [ '<i class="fa fa-arrow-left"></i>', '<i class="fa fa-arrow-right"></i>' ],
                        responsive:{
                        0:{
                            items: 1
                        },
                        480:{
                            items: 2
                        },
                        991:{
                            items: 2
                        }
                    }
                });
            });
        });
    } // end if

    if(freshwp_ajax_object.sticky_sidebar){
    $('.freshwp-sticky-sidebars .freshwp-main-wrapper, .freshwp-sticky-sidebars .freshwp-sidebar-one-wrapper, .freshwp-sticky-sidebars .freshwp-sidebar-two-wrapper').theiaStickySidebar({
        containerSelector: ".freshwp-sticky-sidebars .freshwp-content-inner-wrapper",
        additionalMarginTop: 0,
        additionalMarginBottom: 0,
        minWidth: 1107,
    });
    }

    $(".freshwp-tabbed-content").hide();
    $("ul.freshwp-tabbed-names li:first a").addClass("freshwp-tabbed-current").show();
    $(".freshwp-tabbed-content:first").show();

    $("ul.freshwp-tabbed-names li a").click(function() {
        $("ul.freshwp-tabbed-names li a").removeClass("freshwp-tabbed-current a"); 
        $(this).addClass("freshwp-tabbed-current"); 
        $(".freshwp-tabbed-content").hide(); 
        var freshwpactivetab = $(this).attr("href"); 
        $(freshwpactivetab).fadeIn();
        return false;
    });

});