$(".event-link").click(function (event) {
    event.preventDefault();
    let id = $(this).attr("data-id");
    let title = $(this).attr("data-title");
    console.log("Event: " + id + " - " + title);
    let token = $('input[name="__RequestVerificationToken"]').first().val();
    $(".modal-title").html(title);
    $(".modal-event").html("Loading");
    try {
        $(".modal-event").load("/Fundraising/Event", { event: id, partial: true, __RequestVerificationToken: token });
    }
    catch (error) {
        document.getElementsByClassName("modal-event")[0].textContent = "Something went wrong" + error;
    }
    $("#eventModal").modal('toggle');
});