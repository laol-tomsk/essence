function updateIssueFrom(url, el) {
    $('#all_attributes input, #all_attributes textarea, #all_attributes select').each(function(){
        $(this).data('valuebeforeupdate', $(this).val());
    });
    if (el) {
        $("#form_update_triggered_by").val($(el).attr('id'));
    }
    return $.ajax({
        url: url,
        type: 'post',
        data: $('#issue-form').serialize()
    });
}

function issueClosingListener() {
    $('#issue_status_id').change((event) => {
        const selectedOption = $(event.target).find('option:selected').text();
        const projectId = $('#issue_project_id').find('option:selected').val();
        const activityDefinifionId = $('#issue_activity_definition_id').find('option:selected').val();

        if (selectedOption === 'Closed') {
            // alert('Essence plugin: Please fill complete criterions for the issue.')
            return $.ajax({
                url: "/get_completion_criterions",
                type: 'get',
                dataType: 'script',
                data: {
                    project_id: projectId,
                    activity_definition_id: activityDefinifionId
                },
            })
        }
    })
}

$(document).ready(issueClosingListener);