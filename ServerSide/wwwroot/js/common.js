window.ShowSwal = (type, message) => {
    if (type == "success") {
        Swal.fire(
            'Success!',
            message,
            'success'
        )
    }
    if (type == "error") {
        Swal.fire(
            'Error!',
            message,
            'error'
        )
    }
    if (type == "timer") {
        Swal.fire({
            title: 'Loading',
            html: message,
            timer: 1500,
            timerProgressBar: true,
            didOpen: () => {
                Swal.showLoading()
            },
            willClose: () => {
                Swal.fire('Deleted!', '', 'success')
            }
        })
    }
    //if (type == "deleteConfirm") {
    //    Swal.fire({
    //        title: message,
    //        showCancelButton: true,
    //        confirmButtonText: 'Delete',
    //        type: "warning"
    //    }).then((result) => {
    //        /* Read more about isConfirmed, isDenied below */
    //        if (result.isConfirmed) {
    //            DotNet.invokeMethodAsync('TrainList', 'ConfirmDelete')
    //            Swal.fire('Deleted!', '', 'success')
    //        }
    //    })
    //}
}

window.ShowSwalConfirm = (objRef) => {
    Swal.fire({
        title: 'Are you sure?',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        type: "warning"
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            objRef.invokeMethodAsync('ConfirmDelete');
            /*Swal.fire('Deleted!', '', 'success')*/
        }
    })
}