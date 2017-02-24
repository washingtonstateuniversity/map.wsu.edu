# Animate CSS jQuery Plugin

A jQuery plugin to stack animation and transition over CSS3.

[animate.css]: http://github.com/adelarosab/jquery.animateCSS/

## Getting Started

### Bower
Install with [Bower][bower]
`bower install jquery.animateCSS`

[bower]: http://bower.io/

### Download

Download the [development version][max].

[max]: https://raw.github.com/adelarosab/jquery.animateCSS/master/jquery.animateCSS.js

In your web page:

```html
<script src="jquery.js"></script>
<script src="jquery.animateCSS.js"></script>
<script>
$(document).ready( function(){
  $('#your-id').animateCSS("fadeIn");
});
</script>
```

## Documentation

## Examples

### Basic
```js
$('#your-id').animateCSS('slideOut');
```

### With callback
```js
$('#your-id').animateCSS('slideOut', function(){
    console.log('Boom! Animation Complete');
});
```

### Chain multiple animations
If you use the standard jQuery chaining mechanism, each animation will fire at the same time so you have to include the next animation in the callback.
```js
$('#your-id')
  .animateCSS('slideOut')
  .animateCSS('slideLeft');
```
