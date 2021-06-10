// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function show_hide_password(target) {
	var input = document.getElementById('password-input');
	if (input.getAttribute('type') == 'password') {
		target.classList.add('view');
		input.setAttribute('type', 'text');
	} else {
		target.classList.remove('view');
		input.setAttribute('type', 'password');
	}
	return false;
}
var editor; // use a global for the submit and return data rendering in the examples

const $tableID = $('#table');
const $BTN = $('#export-btn');
const $EXPORT = $('#export');

const newTr = `
<tr>
<td  class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>
<td class="pt-3-half" contenteditable="true" style="white-space: nowrap;"></td>                         
</tr>`;

$('.table-add').on('click', 'i', () => {

    const $clone = $tableID.find('tbody tr').last().clone(true).removeClass('hide table-line');

    if ($tableID.find('tbody tr').length === 0) {

        $('tbody').append(newTr);
    }

    $tableID.find('table').append($clone);
});

$('.table-remove').on('click', 'i', () => {

    var table = document.getElementById('example'); //Задал строго потому что по другому не работало
    if (table.rows.length > 1) {
        table.deleteRow(table.rows.length - 1);
    }
});

$('.table-remove').on('click', 'i', () => {

    $tableID.find('tbody').parents('tr').detach();
});
$tableID.on('click', '.table-up', function () {

    const $row = $(this).parents('tr');

    if ($row.index() === 0) {
        return;
    }

    $row.prev().before($row.get(0));
});

$tableID.on('click', '.table-down', function () {

    const $row = $(this).parents('tr');
    $row.next().after($row.get(0));
});

// A few jQuery helpers for exporting only
jQuery.fn.pop = [].pop;
jQuery.fn.shift = [].shift;

$BTN.on('click', () => {

    const $rows = $tableID.find('tr:not(:hidden)');
    const headers = [];
    const data = [];

    // Get the headers (add special header logic here)
    $($rows.shift()).find('th:not(:empty)').each(function () {

        headers.push($(this).text().toLowerCase());
    });

    // Turn all existing rows into a loopable array
    $rows.each(function () {
        const $td = $(this).find('td');
        const h = {};

        // Use the headers from earlier to name our hash keys
        headers.forEach((header, i) => {

            h[header] = $td.eq(i).text();
        });

        data.push(h);
    });

    // Output the result
    $EXPORT.text(JSON.stringify(data));
});