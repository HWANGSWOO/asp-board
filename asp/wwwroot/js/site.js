// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');

            }
        
    })
};



jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST', // 보내는 방식을 적습니다. get or post
            url: form.action,// 컨트롤러에 보낼 URL을 적습니다. 
            data: new FormData(form), // 컨트롤러로 보낼 데이터를 넣습니다.
            contentType: false, // 보내는 데이터의 인코딩 타입을 적습니다. 
            processData: false,
            success: function (res) { // 통신 결과가 성공일 경우(res)에 반환 될 타입의 데이터가 들어옵니다
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
                    
            },
            error: function (err) {
                console.log(err)
            }
        })
          //기본 양식 제출을 방지하기 위해 여기에 false를 반환
       
    } catch (e) {
        console.log(e)
    }
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm('삭제하시겠습니까?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);


                },
                error: function (err) {
                    console.log(err)
                }
            })

        } catch (e) {
            console.log(e);
        }
         //기본 양식 제출을 방지하기 위해 여기에 false를 반환
        return false;
    }
}