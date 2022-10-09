window.ShowSwal = (type, message) => {
    if (type == "success") {
        Swal.fire({
            position: 'bottom-end',
            icon: 'success',
            title: message,
            showConfirmButton: false,
            timer: 1500
        })  
    }
    if (type == "notAuth") {
        Swal.fire(
            'Warning!',
            message,
            'warning'
        )
    }
    if (type == "error") {
        Swal.fire(
            'Error!',
            message,
            'error'
        )
    }
}

window.ShowSwalToast = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ShowSwalToast2 = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'info',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}