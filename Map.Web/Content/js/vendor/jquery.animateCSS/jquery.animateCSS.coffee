$ = jQuery

$.fn.extend

  animateCSS: (className, callback) ->
    animationEnd = "animationend transitionend"

    return @each () ->
      $this = $ this

      $this.queue (next) ->
        $this
        .addClass className
        .one animationEnd, ->
          callback?()
          next()
