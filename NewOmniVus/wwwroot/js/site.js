const nameRegex = /^[A-Z,ÅÄÖ][a-öA-Ö]+(\-[A-Z,ÅÄÖ][a-ö]+)?\s[A-Z,ÅÄÖ][a-öA-Ö]+(\-[A-Z,ÅÄÖ][a-ö]+)?$/;
const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;




document.getElementById("contact-name").addEventListener("keyup",
    function(e) {
        if (!nameRegex.test(e.target.value)) {
            document.getElementById("contact-name-response").innerText =
                "You must enter a valid firstname and lastname, starting with capital letter";
        } else {
            document.getElementById("contact-name-response").innerText = "";
        }
    });

document.getElementById("contact-email").addEventListener("keyup",
    function(e) {
        if (!emailRegex.test(e.target.value)) {
            document.getElementById("contact-email-response").innerText = "You must enter a valid email";
        } else {
            document.getElementById("contact-email-response").innerText = "";
        }
    });
