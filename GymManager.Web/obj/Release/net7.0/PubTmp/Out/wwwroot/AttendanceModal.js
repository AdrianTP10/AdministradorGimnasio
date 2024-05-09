
const myModal = new bootstrap.Modal(document.getElementById('attendanceModal'));
const input = document.getElementById('userId');
const button = document.getElementById('toggleButton');

const modalBody = document.getElementsByClassName("modal-body")[0];

const showModal = () => {
    modalBody.innerHTML = "Usuario: " + input.value + "<br> Hora actual: " + moment(new Date()).format("HH:mm:ss")
    myModal.show();
    setTimeout(function () {
        myModal.hide();
    }, 3000);
}


input.addEventListener("keydown", (event) => event.key === 'Enter' && showModal());
button.addEventListener("click", showModal);

