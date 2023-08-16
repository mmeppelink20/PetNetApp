$(".campaign-link").click(function (event) {
    event.preventDefault();
    let id = $(this).attr("data-id");
    let title = $(this).attr("data-title");
    let token = $('input[name="__RequestVerificationToken"]').first().val();
    $(".modal-title").html(title);
    $(".modal-campaign").html("Loading");
    try {
        $(".modal-campaign").load("/Fundraising/Campaign", { campaign: id, partial: true, __RequestVerificationToken: token });
    }
    catch (error) {
        document.getElementsByClassName("modal-campaign")[0].textContent = "Something went wrong" + error;
    }
    $("#campaignModal").modal('toggle');
});