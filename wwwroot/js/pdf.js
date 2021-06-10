window.onload = function () {
    document.getElementById("download")
        .addEventListener("click", () => {
            const invoice = this.document.getElementsByTagName("body");
                      
            html2pdf().from(invoice).save();
        })
}