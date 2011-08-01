README.markdown
Impulse Controls (Alpha)
========================
(jQuery Mobile ASP .NET MVC 3 Controls)
---------------------------------------

What's in this release?
-----------------------

Html.ActionButton() - Creates a jQuery Mobile Action Button
-----------------------------------------------------------
Sample syntax:
Sample output:
<a href="index.html" data-role="button" data-icon="delete">Delete</a>
<a href="index.html" data-role="button" data-theme="a">Theme a</a>
<a href="foo.html" data-rel="dialog" data-transition="pop">Open dialog</a> 

Html.ListView() - Creates a jQuery Mobile ListView
--------------------------------------------------
Sample syntax:
Sample output:


For your convenience the Impulse mobile control suite is also available as a nuget package: Impulse.




Controls under consideration:
Html.ButtonGroup()
"grouped buttons"
<div data-role="controlgroup">
<a href="index.html" data-role="button">Yes</a>
<a href="index.html" data-role="button">No</a>
<a href="index.html" data-role="button">Maybe</a>
</div>

"inline buttons"
<div data-inline="true">
	<a href="index.html" data-role="button">Cancel</a>
	<a href="index.html" data-role="button" data-theme="b">Save</a>
</div>

Mobile.HeaderBar()
<div data-role="header" data-position="inline">
	<a href="index.html" data-icon="delete">Cancel</a>
	<h1>Edit Contact</h1>
	<a href="index.html" data-icon="check" data-theme="b">Save</a>
</div>

Mobile.LayoutGrid()
<fieldset class="ui-grid-a">
	<div class="ui-block-a"><button type="submit" data-theme="c">Cancel</button></div>
	<div class="ui-block-b"><button type="submit" data-theme="b">Submit</button></div>	   
</fieldset>