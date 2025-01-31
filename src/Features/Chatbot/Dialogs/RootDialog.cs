﻿namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private readonly IAppoinmentBotService _botService;

    public RootDialog(IAppoinmentBotService botService) : base(nameof(RootDialog))
    {
        _botService = botService;

        var waterfallSteps = new WaterfallStep[]
        {
                ShowNameOfPatients,
                ShowNameOfOffices,
                ShowNameOfServices,
                ShowNameOfDentists,
                ShowAppointmentData
        };

        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        AddDialog(new AdaptiveCardPrompt(nameof(AdaptiveCardPrompt), ValidateChoiceSet));
    }
}
