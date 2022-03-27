const fullNameRegex = /^[A-Z,ÅÄÖ][a-öA-Ö]+(\-[A-Z,ÅÄÖ][a-ö]+)?\s[A-Z,ÅÄÖ][a-öA-Ö]+(\-[A-Z,ÅÄÖ][a-ö]+)?$/
const bookEmailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

var validName = false;

document.getElementById("book-a-meeting-name").addEventListener("keyup",
    function (e) {
        if (!fullNameRegex.test(e.target.value) || e.target.value === null) {
            document.getElementById("book-a-meeting-name-response").innerText =
                "You must enter a valid firstname and lastname, starting with capital letter";
        } else {
            validName = true;
            document.getElementById("book-a-meeting-name-response").innerText = "";
        }
    });

document.getElementById("book-a-meeting-email").addEventListener("keyup",
    function (e) {
        if (!bookEmailRegex.test(e.target.value)) {
            document.getElementById("book-a-meeting-email-response").innerText = "You must enter a valid email";
        } else {
            document.getElementById("book-a-meeting-email-response").innerText = "";
        }
    });

document.getElementById("book-a-meeting-textarea").addEventListener("keyup", function(e) {
    if (e.target.value.length < 3) {
        document.getElementById("book-a-meeting-textarea-response").innerText = "You must enter a message";
    } else {
        document.getElementById("book-a-meeting-textarea-response").innerText = "";
    }
});


document.getElementById("book-a-meeting-form").addEventListener("submit", function(e) {
    alert("No message was sent");
});

function submitForm(event) {
    if (validName === false) {
        event.preventDefault();
        document.getElementById("book-a-meeting-name-response").innerText = "DU HAR GJORT FEL";
    } else {
        document.getElementById("book-a-meeting-name-response").innerText = "DU HAR GJORT RÄTT";
    }
}
