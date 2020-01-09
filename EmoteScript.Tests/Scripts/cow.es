Use:
    - TurnToTarget
    - Motion: Wave
    - InqQuest: CowTip
        QuestSuccess:
            - Tell: You've completed your quest already.
        QuestFailure:
            - InqQuest: cowtipcounter
                QuestSuccess:
                    - InqQuestSolves: cowtipcounter, 300
                        QuestSuccess:
                            - Tell: Good, those cows will be set for awhile.
                            - AwardNoShareXP: 500,000
                            - AddCharacterTitle: ObviouslyBored
                            - DirectBroadcast: You are Obviously Bored.
                            - Give: Tipped Pack Cow (33965)
                            - Tell: Don't tell them I gave that to you.
                            - EraseQuest: cowtipcounter
                            - StampQuest: CowTip
                        QuestFailure:
                            - Tell: You must tip at least 300 cows to complete this quest.
                QuestFailure:
                    - Tell: You are starting the Cow Tipping Quest.
                    - SetQuestCompletions: cowtipcounter, 0